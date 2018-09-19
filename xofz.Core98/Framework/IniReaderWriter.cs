namespace xofz.Framework
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class IniReaderWriter
    {
        public IniReaderWriter(string filePath)
            : this(
                () => File.Exists(filePath)
                    ? File.ReadAllLines(filePath)
                    : new string[0],
                lines => File.WriteAllLines(
                    filePath,
                    EnumerableHelpers.ToArray(lines)))
        {
        }

        public IniReaderWriter(
            Func<string[]> readLines,
            Action<IEnumerable<string>> writeLines)
        {
            this.readLines = readLines;
            this.writeLines = writeLines;
        }

        public virtual ICollection<string> ReadSectionNames()
        {
            return new LinkedList<string>(
                EnumerableHelpers.Select(
                    this.readSectionHeaders(this.readLines()),
                    header => header.Name));
        }

        public virtual ICollection<string> ReadKeysInSection(
            string sectionName)
        {
            var lines = this.readLines();
            var headers = this.readSectionHeaders(
                lines);
            var targetHeader = EnumerableHelpers.FirstOrDefault(
                headers,
                h => h.Name == sectionName);
            return this.readKeysInSectionInternal(
                lines,
                targetHeader);
        }

        public virtual string ReadValue(
            string sectionName, 
            string key)
        {
            var lines = this.readLines();
            var headers = this.readSectionHeaders(
                lines);
            var targetHeader = EnumerableHelpers.FirstOrDefault(
                headers,
                header => header.Name == sectionName);
            if (targetHeader == default(SectionHeader))
            {
                return null;
            }

            if (!this
                .readKeysInSectionInternal(
                    lines, 
                    targetHeader)
                .Contains(key))
            {
                return null;
            }

            var targetLine = default(string);
            int endOfKey;
            foreach (var line in EnumerableHelpers.Skip(
                lines,
                targetHeader.LineNumber))
            {
                endOfKey = line.IndexOf('=');
                if (line.Substring(0, endOfKey) == key)
                {
                    targetLine = line;
                    break;
                }
            }

            if (targetLine == default(string))
            {
                return null;
            }

            var startIndexOfValue = targetLine.IndexOf('=') + 1;
            int valueLength;
            var indexOfSemicolon = targetLine.IndexOf(';');
            if (indexOfSemicolon > -1)
            {
                valueLength = indexOfSemicolon - startIndexOfValue;
                goto finish;
            }

            valueLength = targetLine.Length - startIndexOfValue;

            finish:
            return targetLine
                .Substring(
                    startIndexOfValue, 
                    valueLength)
                .TrimEnd();
        }

        public virtual void ChangeValue(
            string sectionName,
            string key,
            string newValue)
        {
            var lines = this.readLines();
            var sectionHeaders = this.readSectionHeaders(lines);
            var targetHeader = default(SectionHeader);
            var nextHeaderCounter = 1;
            foreach (var header in sectionHeaders)
            {
                if (header.Name == sectionName)
                {
                    targetHeader = header;
                    break;
                }

                ++nextHeaderCounter;
            }
            if (targetHeader == default(SectionHeader))
            {
                return;
            }

            if (!this.readKeysInSectionInternal(lines, targetHeader)
                .Contains(key))
            {
                return;
            }

            var nextHeader = EnumerableHelpers.FirstOrDefault(
                EnumerableHelpers.Skip(
                    sectionHeaders,
                    nextHeaderCounter));
            var startOfSection = targetHeader.LineNumber;
            var endOfSection = lines.Length;
            if (nextHeader != default(SectionHeader))
            {
                endOfSection = nextHeader.LineNumber - 1;
            }

            for (var i = startOfSection; i < endOfSection; ++i)
            {
                if (!lines[i].StartsWith(key))
                {
                    continue;
                }

                var sb = new StringBuilder(
                    lines[i]);
                var valueIndex = lines[i].IndexOf('=') + 1;
                var indexOfComment = lines[i].IndexOf(';');
                if (indexOfComment < 0)
                {
                    sb.Replace(
                        lines[i].Substring(
                            valueIndex),
                        newValue);
                }
                else
                {
                    sb.Replace(
                        lines[i].Substring(
                            valueIndex,
                            indexOfComment - valueIndex),
                        newValue);
                }

                lines[i] = sb.ToString();
                this.writeLines(lines);
                return;
            }
        }

        protected virtual ICollection<SectionHeader> readSectionHeaders(
            IEnumerable<string> lines)
        {
            ICollection<SectionHeader> sectionHeaders
                = new LinkedList<SectionHeader>();
            var lineNumber = 0;
            foreach (var line in lines)
            {
                ++lineNumber;
                if (!line.StartsWith("["))
                {
                    continue;
                }

                if (!line.Contains("]"))
                {
                    continue;
                }

                var closingBracketIndex = line.IndexOf(']');
                if (line
                    .Substring(0, closingBracketIndex)
                    .Contains(";"))
                {
                    continue;
                }

                var sectionName = line.Substring(
                    1,
                    closingBracketIndex - 1);
                sectionHeaders.Add(
                    new SectionHeader
                    {
                        Name = sectionName,
                        LineNumber = lineNumber
                    });
            }

            return sectionHeaders;
        }

        private ICollection<string> readKeysInSectionInternal(
            string[] lines,
            SectionHeader targetHeader)
        {
            ICollection<string> keys = new LinkedList<string>();
            if (targetHeader == default(SectionHeader))
            {
                return keys;
            }

            var headers = this.readSectionHeaders(lines);
            if (!EnumerableHelpers.Contains(
                EnumerableHelpers.Select(
                    headers,
                    header => header.Name),
                targetHeader.Name))
            {
                return keys;
            }
                        
            var lineNumber = targetHeader.LineNumber;
            int lastLineIndex;
            if (EnumerableHelpers.Last(
                EnumerableHelpers.Select(
                    headers,
                    header => header.Name)) == targetHeader.Name)
            {
                lastLineIndex = lines.Length - 1;
                goto readKeys;
            }

            var headerCounter = 1;
            foreach (var header in headers)
            {
                if (header.Name == targetHeader.Name)
                {
                    break;
                }

                ++headerCounter;
            }

            var nextHeader = EnumerableHelpers.FirstOrDefault(
                EnumerableHelpers.Skip(
                    headers, headerCounter));
            lastLineIndex = nextHeader?.LineNumber - 1
                ?? lines.Length - 1;


            readKeys:
            for (var i = lineNumber; i <= lastLineIndex; ++i)
            {
                var indexOfSemicolon = lines[i].IndexOf(';');
                if (indexOfSemicolon == 0)
                {
                    continue;
                }

                var indexOfEquals = lines[i].IndexOf('=');
                if (indexOfEquals < 0)
                {
                    continue;
                }

                if (indexOfSemicolon > -1 &&
                    indexOfSemicolon < indexOfEquals)
                {
                    continue;
                }

                var keyName = lines[i]
                    .Substring(0, indexOfEquals);
                keys.Add(keyName);
            }

            return keys;
        }

        private readonly Func<string[]> readLines;
        private readonly Action<IEnumerable<string>> writeLines;

        protected class SectionHeader
        {
            public virtual string Name { get; set; }

            public virtual int LineNumber { get; set; }
        }
    }
}
