using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace CRM.Application.StaticValidation
{

    public class StaticValidation : ValidationRule
    {
        public string _pattern;

        public string Pattern 
        {
            get { return _pattern; }
            set { _pattern = value; }
        }

        public string _validationType { get; set; }

        public string ValidationType 
        {
            get { return _validationType; }
            set { _validationType = value; }
        }


       // public int PersonType { get; set; }

        private PersonTypeClass _personTypeClassProperty;

        public PersonTypeClass PersonTypeClassProperty
        {
            get { return _personTypeClassProperty; }
            set { _personTypeClassProperty = value; }
        }

     
        
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) 
        {
            bool validationResult = true;
             string validationResultMessge = string.Empty;

            switch(_validationType)
            {
                case "Pattern":
                    {
                        if (_pattern == null) return ValidationResult.ValidResult;

                        if (!(value is string)) return new ValidationResult(false, "الگو معتبر نمی باشد");

                        Regex _patternRegex = new Regex(_pattern);
                        Match match = _patternRegex.Match(value.ToString());
                        if (match.Success == false)
                        {
                            validationResult = false;
                            validationResultMessge = "داده وارد شده معتبر نمی باشد" ;
                        }
                        break;
                    }

                case "NotNull":
                    {
                        if (value == null)
                        {
                            validationResult = false;
                            validationResultMessge = "فیلد مورد نظر نمی تواند خالی باشد";
                        }
                        break;
                    }
                case "NationalCode":
                    {
                        if (!(value is string))
                        {
                            validationResult = false;
                            validationResultMessge = "لطفا کد ملی صحیح وارد نمایید";
                        }

                        if (string.IsNullOrWhiteSpace(value.ToString()) )
                        {
                            validationResult = false;
                            validationResultMessge = "لطفا کد ملی را وارد نمایید";
                        }

                        if (!(Regex.IsMatch(value.ToString(), @"\d")))
                        {
                            validationResult = false;
                            validationResultMessge = "لطفا کد ملی را  صحیح وارد نمایید";
                        }

                        string input = value.ToString();
                        if (_personTypeClassProperty.PersonType == 0 && validationResult) // اشخاص حقیقی
                        {
                            

                            if (input.Length == 10)
                            {
                                if (input == "1111111111" || input == "0000000000" ||
                                    input == "2222222222" || input == "3333333333" ||
                                    input == "4444444444" || input == "5555555555" ||
                                    input == "6666666666" || input == "7777777777" ||
                                    input == "8888888888" || input == "9999999999")
                                {
                                    validationResult = false;
                                    validationResultMessge = "کد ملی وارد شده معتبر نمی باشد";
                                }
                                else
                                {

                                    int A = int.Parse(input[9].ToString());
                                    int B = int.Parse(input[0].ToString()) * 10 +
                                            int.Parse(input[1].ToString()) * 9 +
                                            int.Parse(input[2].ToString()) * 8 +
                                            int.Parse(input[3].ToString()) * 7 +
                                            int.Parse(input[4].ToString()) * 6 +
                                            int.Parse(input[5].ToString()) * 5 +
                                            int.Parse(input[6].ToString()) * 4 +
                                            int.Parse(input[7].ToString()) * 3 +
                                            int.Parse(input[8].ToString()) * 2;

                                    int C = B - (B / 11) * 11;

                                    if ((C == 0 && C == A) || (C == 1 && A == 1) || (C > 1 && (A == 11 - C)))
                                    { 
                                    }
                                    else
                                    {
                                        validationResult = false;
                                        validationResultMessge = "کد ملی وارد شده معتبر نمی باشد";
                                    }

                                }
                            }
                            else
                            {
                                validationResult = false;
                                validationResultMessge = "کد ملی وارد شده معتبر نمی باشد";
                            }
                        }
                        else if (_personTypeClassProperty.PersonType == 1  && validationResult) // اشخاص حقوقی
                        {

                            if (input.Length == 11)
                            {
                                if (input == "11111111111" || input == "00000000000" ||
                                    input == "22222222222" || input == "33333333333"||
                                    input == "44444444444" || input == "55555555555" ||
                                    input == "66666666666" || input == "77777777777" ||
                                    input == "88888888888" || input == "99999999999")
                                {
                                    validationResult = false;
                                    validationResultMessge = "شناسه ملی وارد شده معتبر نمی باشد";
                                }
                                else
                                {
                                    int chechSum = Convert.ToInt16(input[11]);
                                    int dec = Convert.ToInt16(input[10]) + 2;

                                    int[] z = new int[] { 29, 27, 23, 19, 17 };
                                    int sum = 0;

                                    for (byte i = 0; i < 10; i++)
                                        sum += (dec + Convert.ToInt16(input[i])) * z[i % 5];

                                    sum = sum % 11;

                                    if (sum == 10)
                                        sum = 0;

                                    if (chechSum != sum)
                                    {
                                        validationResult = false;
                                        validationResultMessge = "شناسه ملی وارد شده معتبر نمی باشد";
                                    }
                                }
                            }
                        }
                        break;
                    }

                case "PostalCode":
                    {
                        if (!(value is string)) 
                        {
                            validationResult = false;
                            validationResultMessge = "کد پستی وارد شده معتبر نمی باشد";
                        }

                        if (string.IsNullOrWhiteSpace(value.ToString()))
                        {
                            validationResult = false;
                            validationResultMessge = "لطفا کد پستی را وارد نمایید";
                        }

                        if (!(Regex.IsMatch(value.ToString(), @"\d")))
                        {
                            validationResult = false;
                            validationResultMessge = "لطفا کد پستی را  صحیح وارد نمایید";
                        }
                        

                        string input = value.ToString();

                        if (!(validationResult && input.Length == 10))
                        {
                            validationResult = false;
                            validationResultMessge = "کد پستی وارد شده معتبر نمی باشد";
                        }

                        break;
                    }

            }

            if(validationResult)
                return ValidationResult.ValidResult;
            else
                return new ValidationResult(validationResult, validationResultMessge);
        }


    }


    public class PersonTypeClass : DependencyObject
    {
        public int PersonType
        {
            get { return (int)GetValue(PersonTypeProperty); }
            set { SetValue(PersonTypeProperty, value); }
        }

        public static readonly DependencyProperty PersonTypeProperty = DependencyProperty.Register("PersonType", typeof(int), typeof(PersonTypeClass), new PropertyMetadata(0));

    }
}
