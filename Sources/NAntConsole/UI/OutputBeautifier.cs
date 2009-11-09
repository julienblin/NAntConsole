using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CDS.Framework.Tools.NAntConsole.UI
{
    internal class OutputBeautifier
    {
        readonly IList<RegexMatcher> regexMatchers = new List<RegexMatcher>();

        public OutputBeautifier()
        {
            regexMatchers.Add(new RegexMatcher(@"BUILD SUCCEEDED", Color.Green));
            regexMatchers.Add(new RegexMatcher(@"BUILD FAILED", Color.Red));
            regexMatchers.Add(new RegexMatcher(@"\[[\w-_\.]+\]\s*WARNING:", Color.Yellow));
            // SharpSvn output
            regexMatchers.Add(new RegexMatcher(@"^Conflicted:", Color.Yellow));
            regexMatchers.Add(new RegexMatcher(@"^[\w-_\.]+Failed\s:\s", Color.Red)); 
            regexMatchers.Add(new RegexMatcher(@"^[\w-_\.]+:$", Color.Yellow));
            
            // Passwords must not be displayed
            regexMatchers.Add(new RegexMatcher(@"password", Color.Empty, "password-field protected line."));
            regexMatchers.Add(new RegexMatcher(@"pwd", Color.Empty, "password-field protected line."));
        }

        public void Print(string output, RichTextBox textBox)
        {
            string lastLine = GetLastLine(textBox);
            if (!lastLine.Equals(string.Empty, StringComparison.InvariantCulture))
            {
                textBox.AppendText(Environment.NewLine);
            }

            bool textAppended = false;
            foreach (RegexMatcher regexMatcher in regexMatchers)
            {
                if (regexMatcher.Match(output))
                {
                    if (regexMatcher.Color == Color.Empty)
                    {
                        if (!string.IsNullOrEmpty(regexMatcher.SubstitutionString))
                        {
                            textBox.AppendText(regexMatcher.SubstitutionString);
                        }
                    }
                    else
                    {
                        int selectionBegin = textBox.Text.Length;
                        textBox.AppendText(output);
                        int selectionEnd = textBox.Text.Length;
                        textBox.Select(selectionBegin, selectionEnd);
                        textBox.SelectionColor = regexMatcher.Color;
                    }

                    textAppended = true;
                    break;
                }
            }

            if (!textAppended)
                textBox.AppendText(output);

            textBox.DeselectAll();
            ScrollHelper.ScrollToBottomLeft(textBox);
        }

        private static string GetLastLine(TextBoxBase textBox)
        {
            return textBox.Lines.Length == 0 ? string.Empty : textBox.Lines[textBox.Lines.Length - 1];
        }

        class RegexMatcher
        {
            private readonly Regex regexToTest;
            private readonly Color color;
            private readonly string substitutionString;

            public RegexMatcher(string regexToTest, Color color) : this(regexToTest, color, null)
            {
            }

            public RegexMatcher(string regexToTest, Color color, string substitutionString)
            {
                this.regexToTest = new Regex(regexToTest, RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
                this.color = color;
                this.substitutionString = substitutionString;
            }

            public bool Match(string nantOutput)
            {
                return regexToTest.Match(nantOutput).Success;
            }

            public Color Color
            {
                get { return color; }
            }

            public string SubstitutionString
            {
                get { return substitutionString; }
            }
        }
    }
}
