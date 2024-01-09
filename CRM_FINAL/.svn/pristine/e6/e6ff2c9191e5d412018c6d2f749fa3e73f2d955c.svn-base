using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class Calculate
    {
        public static CompilerErrorCollection CompiledFormula(string formula)
        {
            formula = Data.Calculate.ReplaceString(formula);
            if (!Checkformula(formula))
            {
                throw new Exception("در فرمول از کارکتر غیر مجاز استفاده شده است لطفا فرمول را اسلاح کنید");
            }
            CompilerResults resultCompiled;
            using (Microsoft.CSharp.CSharpCodeProvider csharpCodeProvider = new Microsoft.CSharp.CSharpCodeProvider())
            {
                resultCompiled = csharpCodeProvider.CompileAssemblyFromSource(
                    new System.CodeDom.Compiler.CompilerParameters()
                    {
                        GenerateInMemory = true,
                        CompilerOptions = "/optimize"
                    },
                    CreateStringClass(formula)

                );



            }

            return resultCompiled.Errors;

        }

        private static string CreateStringClass(string formula)
        {
            
            return @"public class DynamicCodeProvider 
                       {
                           public double Execute() 
                           {  
                              double result = 0;
                              int outBoundMeter = 0;
                              long CableMeter = 0;
                              byte ZeroBlock = 0; "
                              + formula +
                             @"  return result;
                           }
                       }";
        }

        private static bool Checkformula(string formula)
        {
            string output = System.Text.RegularExpressions.Regex.Replace(formula, @"[\d-]", string.Empty);
            output = output.Replace("result", string.Empty);
            output = output.Replace("outBoundMeter", string.Empty);
            output = output.Replace("CableMeter", string.Empty); 
            output = output.Replace("ZeroBlock", string.Empty);
            output = output.Replace("FirstZero", string.Empty);
            output = output.Replace("LimitLess", string.Empty);
            output = output.Replace("SecondZero", string.Empty);
            output = output.Replace("if", string.Empty);
            output = output.Replace("else", string.Empty);
            output = output.Replace(";", string.Empty);
            output = output.Replace("%", string.Empty);
            output = output.Replace("*", string.Empty);
            output = output.Replace(">", string.Empty);
            output = output.Replace("/", string.Empty);
            output = output.Replace("+", string.Empty);
            output = output.Replace(">=", string.Empty);
            output = output.Replace("<", string.Empty);
            output = output.Replace("<=", string.Empty);
            output = output.Replace("{", string.Empty);
            output = output.Replace("}", string.Empty);
            output = output.Replace("(", string.Empty);
            output = output.Replace(")", string.Empty);
            output = output.Replace("=", string.Empty);
            output = output.Replace("==", string.Empty);
            output = output.Replace("\n", string.Empty);
            output = output.Replace("\r", string.Empty);
            output = output.Replace(" ", string.Empty);
            output = output.Replace(".", string.Empty);
            output = output.Replace("!", string.Empty);


            if (output == string.Empty)
                return true;
            else
                return false;



        }


        public static string ReplaceString(string Formula)
        {

            Formula = Formula.Replace("Round", "(decimal)System.Math.Round");
            Formula = Formula.Replace("FirstZero", Convert.ToString((byte)DB.ClassTelephone.FirstZeroBlock));
            Formula = Formula.Replace("SecondZero", Convert.ToString((byte)DB.ClassTelephone.SecondZeroBlock));
            Formula = Formula.Replace("LimitLess", Convert.ToString((byte)DB.ClassTelephone.LimitLess));

            return Formula;
        }

        public static double Execute(string formula)
        {
            formula = Data.Calculate.ReplaceString(formula);

            if (!Checkformula(formula))
            {
                throw new Exception("در فرمول از کارکتر غیر مجاز استفاده شده است لطفا فرمول را اسلاح کنید");
            }
            double result = 0;
            CompilerResults resultCompiled;
            using (Microsoft.CSharp.CSharpCodeProvider csharpCodeProvider = new Microsoft.CSharp.CSharpCodeProvider())
            {
                resultCompiled = csharpCodeProvider.CompileAssemblyFromSource(
                    new System.CodeDom.Compiler.CompilerParameters()
                    {
                        GenerateInMemory = true,

                        CompilerOptions = "/optimize"
                    },
                    CreateStringClass(formula)
                );



            }

            if (resultCompiled.Errors.Count > 0)
            {
                throw new Exception("در محاسبه فرمول خطا وجود دارد لطفا فرمول را اصلاح کنید.");
            }
            else
            {
                var type = resultCompiled.CompiledAssembly.GetType("DynamicCodeProvider");
                var method = type.GetMethod("Execute");

                var action = (Func<double>)Delegate.CreateDelegate(typeof(Func<double>), null, method);
                result = action.Invoke();

            }
            return result;





        }
    }
}
