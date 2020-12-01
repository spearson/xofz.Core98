namespace xofz.Framework
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using EH = xofz.EnumerableHelpers;

    public class IniReaderWriter
    {
        public IniReaderWriter(
            string filePath)
            : this(
                () => File.Exists(filePath)
                    ? File.ReadAllLines(filePath)
                    : new string[0],
                lines => File.WriteAllLines(
                    filePath,
                    EH.ToArray(lines)))
        {
        }

        public IniReaderWriter(
            Gen<string[]> readLines,
            Do<IEnumerable<string>> writeLines)
        {
            this.readLines = readLines;
            this.writeLines = writeLines;
        }

        public virtual ICollection<string> ReadSectionNames()
        {
            return new LinkedList<string>(
                EH.Select(
                    this.readSectionHeaders(
                        this.readLines()),
                    header => header.Name));
        }

        public virtual ICollection<string> ReadKeysInSection(
            string sectionName)
        {
            string[] lines;
            try
            {
                lines = this.readLines();
            }
            catch
            {
                return new LinkedList<string>();
            }
            
            var headers = this.readSectionHeaders(
                lines);
            var targetHeader = EH.FirstOrDefault(
                headers,
                h => h.Name == sectionName);
            return this.readKeysInSectionProtected(
                lines,
                targetHeader);
        }

        public virtual string ReadValue(
            string sectionName, 
            string key)
        {
            return this.readValueProtected(
                sectionName,
                key,
                false);
        }

        public virtual string ReadEntireValue(
            string sectionName,
            string key)
        {
            return this.readValueProtected(
                sectionName,
                key,
                true);
        }

        public virtual void ChangeValue(
            string sectionName,
            string key,
            string newValue)
        {
            string[] lines;
            try
            {
                lines = this.readLines();
            }
            catch
            {
                return;
            }

            var sectionHeaders = this.readSectionHeaders(lines);
            SectionHeader targetHeader = default;
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
            if (targetHeader == default)
            {
                return;
            }

            if (!this.readKeysInSectionProtected(lines, targetHeader)
                .Contains(key))
            {
                return;
            }

            var nextHeader = EH.FirstOrDefault(
                EH.Skip(
                    sectionHeaders,
                    nextHeaderCounter));
            var startOfSection = targetHeader.LineNumber;
            var endOfSection = lines.Length;
            if (nextHeader != default)
            {
                endOfSection = nextHeader.LineNumber - 1;
            }

            for (var i = startOfSection; i < endOfSection; ++i)
            {
                var line = lines[i];
                if (line == null)
                {
                    continue;
                }

                if (!line.StartsWith(key))
                {
                    continue;
                }

                var sb = new StringBuilder(
                    line);
                var valueIndex = line.IndexOf('=') + 1;
                var indexOfComment = line.IndexOf(';');
                if (indexOfComment < 0)
                {
                    sb.Replace(
                        line.Substring(
                            valueIndex),
                        newValue);
                }
                else
                {
                    sb.Replace(
                        line.Substring(
                            valueIndex,
                            indexOfComment - valueIndex),
                        newValue);
                }

                lines[i] = sb.ToString();
                try
                {
                    this.writeLines(lines);
                }
                catch
                {
                    return;
                }
                
                return;
            }
        }

        protected virtual ICollection<SectionHeader> readSectionHeaders(
            IEnumerable<string> lines)
        {
            ICollection<SectionHeader> sectionHeaders
                = new LinkedList<SectionHeader>();
            if (lines == null)
            {
                return sectionHeaders;
            }

            var lineNumber = 0;
            foreach (var line in lines)
            {
                ++lineNumber;
                if (line == null)
                {
                    continue;
                }

                if (!line.StartsWith(@"["))
                {
                    continue;
                }

                if (!line.Contains(@"]"))
                {
                    continue;
                }

                var closingBracketIndex = line.IndexOf(']');
                if (line
                    .Substring(0, closingBracketIndex)
                    .Contains(@";"))
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

        protected virtual ICollection<string> readKeysInSectionProtected(
            string[] lines,
            SectionHeader targetHeader)
        {
            ICollection<string> keys = new LinkedList<string>();
            if (targetHeader == default)
            {
                return keys;
            }

            var headers = this.readSectionHeaders(lines);
            if (!EH.Contains(
                EH.Select(
                    headers,
                    header => header.Name),
                targetHeader.Name))
            {
                return keys;
            }
                        
            var lineNumber = targetHeader.LineNumber;
            int lastLineIndex;
            if (EH.Last(
                EH.Select(
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

            var nextHeader = EH.FirstOrDefault(
                EH.Skip(
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

        protected virtual string readValueProtected(
            string sectionName,
            string key,
            bool readEntireValue)
        {
            var lines = this.readLines();
            var headers = this.readSectionHeaders(
                lines);
            var targetHeader = EH.FirstOrDefault(
                headers,
                header => header.Name == sectionName);
            if (targetHeader == default)
            {
                return null;
            }

            if (!this
                .readKeysInSectionProtected(
                    lines,
                    targetHeader)
                .Contains(key))
            {
                return null;
            }

            string targetLine = default;
            int endOfKey;
            foreach (var line in EH.Skip(
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

            if (targetLine == default)
            {
                return null;
            }

            var startIndexOfValue = targetLine.IndexOf('=') + 1;
            int valueLength;
            var indexOfSemicolon = targetLine.IndexOf(';');
            if (indexOfSemicolon > -1 && !readEntireValue)
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

        protected readonly Gen<string[]> readLines;
        protected readonly Do<IEnumerable<string>> writeLines;

        protected class SectionHeader
        {
            public virtual string Name { get; set; }

            public virtual int LineNumber { get; set; }
        }
    }
}
