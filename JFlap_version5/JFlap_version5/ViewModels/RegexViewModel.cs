using JFlap_version5.Commands;
using JFlap_version5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JFlap_version5.ViewModels
{
    public class RegexViewModel
    {
        #region Properties
        public RegexTextField PatternField { get; set; }
        public RegexTextField StringField { get; set; }
        public RegexTextField ResultField { get; set; }
        public Command VerifyTextCommand { get; private set; }
        #endregion

        #region Constructor
        public RegexViewModel()
        {
            VerifyTextCommand = new Command( RegexFunctionality, CanVerify);
            PatternField = new RegexTextField();
            StringField = new RegexTextField();
            ResultField = new RegexTextField();
        }
        #endregion

        #region Functionality
        private bool CanVerify(object parameter)
        {
            return true;
        }

        private void RegexFunctionality(object paramter)
        {
            ResultField.Pattern = "";
            if(PatternField.Pattern != null)
            {
                Console.WriteLine(PatternField.Pattern);
                Console.WriteLine(StringField.Pattern);
                try
                {
                    /* If we remove the try-catch, it will throw an exception at line 48 if 
                     * the pattern is not correct. We need it to validate the user's input.*/
                    Regex regex = new Regex(PatternField.Pattern);
                    if (StringField.Pattern != null)
                    {
                        MatchCollection matches = regex.Matches(StringField.Pattern);
                        if(matches.Count != 0)
                        {
                            ResultField.Pattern = "The string and the pattern match.\n";
                            foreach (var match in matches)
                            {
                                ResultField.Pattern += match.ToString() + "\n";
                            }
                        }
                        else
                        {
                            ResultField.Pattern = "The string is not a match...";
                        }
                    }
                }
                catch (Exception)
                {
                    // the pattern is incorrect.
                    ResultField.Pattern = "Check the regular expression,\nit is not correct.\n";
                }
            }   
        }
        #endregion
    }
}
