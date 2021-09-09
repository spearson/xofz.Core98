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
                    : new string[zero],
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
            return XLinkedList<string>.Create(
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
                return new XLinkedList<string>();
            }
            
            var headers = this.readSectionHeaders(
                lines);
            var targetHeader = EH.FirstOrNull(
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
            SectionHeader targetHeader = null;
            int nextHeaderCounter = one;
            foreach (var header in sectionHeaders)
            {
                if (header.Name == sectionName)
                {
                    targetHeader = header;
                    break;
                }

                ++nextHeaderCounter;
            }
            if (targetHeader == null)
            {
                return;
            }

            if (!this.readKeysInSectionProtected(lines, targetHeader)
                .Contains(key))
            {
                return;
            }

            var nextHeader = EH.FirstOrNull(
                EH.Skip(
                    sectionHeaders,
                    nextHeaderCounter));
            var startOfSection = targetHeader.LineNumber;
            var endOfSection = lines.Length;
            if (nextHeader != null)
            {
                endOfSection = nextHeader.LineNumber - one;
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
                var valueIndex = line.IndexOf('=') + one;
                var indexOfComment = line.IndexOf(';');
                if (indexOfComment < zero)
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
                = new XLinkedList<SectionHeader>();
            if (lines == null)
            {
                return sectionHeaders;
            }

            int lineNumber = zero;
            const string 
                leftBracket = @"[",
                rightBracket = @"]",
                semicolon = @";";
            const char rightBracketC = ']';
            foreach (var line in lines)
            {
                ++lineNumber;
                if (line == null)
                {
                    continue;
                }

                if (!line.StartsWith(leftBracket))
                {
                    continue;
                }

                if (!line.Contains(rightBracket))
                {
                    continue;
                }

                var closingBracketIndex = line.IndexOf(rightBracketC);
                if (line
                    .Substring(zero, closingBracketIndex)
                    .Contains(semicolon))
                {
                    continue;
                }

                var sectionName = line.Substring(
                    one,
                    closingBracketIndex - one);
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
            ICollection<string> keys = new XLinkedList<string>();
            if (targetHeader == null)
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
                lastLineIndex = lines.Length - one;
                goto readKeys;
            }

            int headerCounter = one;
            foreach (var header in headers)
            {
                if (header.Name == targetHeader.Name)
                {
                    break;
                }

                ++headerCounter;
            }

            var nextHeader = EH.FirstOrNull(
                EH.Skip(
                    headers, headerCounter));
            lastLineIndex = nextHeader?.LineNumber - one
                ?? lines.Length - one;


            readKeys:
            for (var i = lineNumber; i <= lastLineIndex; ++i)
            {
                const char
                    semicolon = ';',
                    equals = '=';
                var indexOfSemicolon = lines[i].IndexOf(semicolon);
                if (indexOfSemicolon == zero)
                {
                    continue;
                }

                var indexOfEquals = lines[i].IndexOf(equals);
                if (indexOfEquals < zero)
                {
                    continue;
                }

                if (indexOfSemicolon > minusOne &&
                    indexOfSemicolon < indexOfEquals)
                {
                    continue;
                }

                var keyName = lines[i]
                    .Substring(zero, indexOfEquals);
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
            var targetHeader = EH.FirstOrNull(
                headers,
                header => header.Name == sectionName);
            if (targetHeader == null)
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

            string targetLine = null;
            int endOfKey;
            const char equals = '=';
            foreach (var line in EH.Skip(
                lines,
                targetHeader.LineNumber))
            {
                endOfKey = line.IndexOf(equals);
                if (line.Substring(zero, endOfKey) == key)
                {
                    targetLine = line;
                    break;
                }
            }

            if (targetLine == null)
            {
                return null;
            }

            var startIndexOfValue = targetLine.IndexOf(equals) + one;
            int valueLength;
            const char semicolon = ';';
            var indexOfSemicolon = targetLine.IndexOf(semicolon);
            if (indexOfSemicolon > minusOne && !readEntireValue)
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
        protected const short
            minusOne = -1;
        protected const byte
            zero = 0,
            one = 1;

        protected class SectionHeader
        {
            public virtual string Name { get; set; }

            public virtual int LineNumber { get; set; }
        }
    }
}
