// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnSorter.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace xofz.UI.Forms.Internal
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows.Forms;

    // lots of code taken from http://www.codeproject.com/Articles/18440/DataGridView-Multi-column-Sort
    internal sealed class ColumnSorter : IComparer
    {
        public ColumnSorter(DataGridView grid)
        {
            this.grid = grid;
            this.maxSortColumns = 0;
            this.sortedColumns = new List<ColumnDefinition>(this.maxSortColumns);
        }

        int IComparer.Compare(object x, object y)
        {
            var lhs = x as DataGridViewRow;
            var rhs = y as DataGridViewRow;

            return this.compare(lhs.Cells, rhs.Cells);
        }

        public int MaxSortColumns
        {
            get
            {
                return this.sortedColumns.Capacity;
            }

            set
            {
                if (this.sortedColumns.Count > value)
                {
                    this.sortedColumns.RemoveRange(value - 1, this.sortedColumns.Count);
                }

                this.sortedColumns.Capacity = value;
            }
        }

        public void SetSortColumn(int columnIndex, Keys modifierKeys)
        {
            bool keepSamePriority = (modifierKeys & Keys.Control) == Keys.Control;
            ColumnDefinition columnDefinition;

            if (this.sortedColumns.Count > 0 && !keepSamePriority)
            {
                // Erase the current sort glyph.
                columnDefinition = this.sortedColumns[0];
                this.grid.Columns[columnDefinition.columnNumber].HeaderCell.SortGlyphDirection = SortOrder.None;
            }

            int sortPriority = this.sortedColumns.FindIndex(cd => cd.columnNumber == columnIndex);

            if (sortPriority != -1)
            {
                this.ReverseSort(keepSamePriority, sortPriority);
                return;
            }

            // Column not found in list.
            // This column is not being sorted at present.

            // If got to limit of num sorted columns, remove the current last one.
            if (this.maxSortColumns > 0 && this.sortedColumns.Count == this.sortedColumns.Capacity)
            {
                this.sortedColumns.RemoveAt(this.sortedColumns.Count - 1);
            }

            columnDefinition = new ColumnDefinition(columnIndex, SortOrder.Ascending);
            if (keepSamePriority)
            {
                this.sortedColumns.Add(columnDefinition);
                if (this.sortedColumns.Count > 1)
                {
                    return;
                }
            }
            else
            {
                this.sortedColumns.Insert(0, columnDefinition);
            }

            this.grid.Columns[columnDefinition.columnNumber].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
        }

        private void ReverseSort(bool keepSamePriority, int sortPriority)
        {
            ColumnDefinition columnDefinition = this.sortedColumns[sortPriority];

            // The column is already being sorted.
            SortOrder sortOrder;
            if (sortPriority == 0 || keepSamePriority)
            {
                // There is no need to change where it is in the list.
                columnDefinition.ascending = !columnDefinition.ascending;
                this.sortedColumns[sortPriority] = columnDefinition;

                sortOrder = columnDefinition.ascending ? SortOrder.Ascending : SortOrder.Descending;
                if (sortPriority != 0) return;
            }
            else
            {
                // Promote this column to be the first.
                for (int loop = sortPriority; loop > 0; --loop)
                {
                    this.sortedColumns[loop] = this.sortedColumns[loop - 1];
                }
                
                // Promoted columns are always sorted ascending.
                columnDefinition.ascending = true;
                this.sortedColumns[0] = columnDefinition;
                sortOrder = SortOrder.Ascending;
            }

            this.grid.Columns[columnDefinition.columnNumber].HeaderCell.SortGlyphDirection = sortOrder;
        }

        public string SortOrderDescription
        {
            get
            {
                var info =
                    EnumerableHelpers.Select(
                        this.sortedColumns,
                        column =>
                        this.grid.Columns[column.columnNumber].HeaderText + (column.ascending ? " ASC" : " DESC"));

                return "Sorted by " + string.Join(
                           ", ", 
                           EnumerableHelpers.ToArray(info));
            }
        }

        private int compare(DataGridViewCellCollection lhs, DataGridViewCellCollection rhs)
        {
            foreach (var colDefn in this.sortedColumns)
            {
                var compareResult = Comparer<object>.Default.Compare(
                    lhs[colDefn.columnNumber].Value,
                    rhs[colDefn.columnNumber].Value);

                if (compareResult != 0)
                {
                    return colDefn.ascending ? compareResult : -compareResult;
                }
            }

            // These two rows are indistinguishable.
            return 0;
        }

        private readonly DataGridView grid;
        private readonly List<ColumnDefinition> sortedColumns;
        private readonly int maxSortColumns;
    }
}
