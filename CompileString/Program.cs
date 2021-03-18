using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileString
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variable
            string VariableName = "My_Variable";
            if(IsValidVariableName(VariableName))
                Console.WriteLine(VariableName+"\t- OK");
            else
                Console.WriteLine(VariableName+"\t- Invalid");


            //Function
            string FunctionDeclaration = "public void CheckThisStringstring var1, int var2)";
            if(IsValidFunction(FunctionDeclaration).IsValid)
                Console.WriteLine("Fucntion\t- OK");
            else
                Console.WriteLine("Function\t- Invalid");

            Console.ReadKey();

        }

        public static MethodModel IsValidFunction(string givenFunction)
        {
            string[] split = givenFunction.Split(' ', '(', ',', ')');
            string[] ParameterPart = givenFunction.Split('(', ')');

            MethodModel FunctionModel = new MethodModel();
            if (IsValidAM(split[0]))
                FunctionModel.AccessModifier = split[0];
            else
                FunctionModel.IsValid = false;

            if (IsValidType(split[1]))
                FunctionModel.ReturnType = split[1];
            else
                FunctionModel.IsValid = false;

            if (IsValidVariableName(split[2]))
                FunctionModel.MethodName = split[2];
            else
                FunctionModel.IsValid = false;

            string Parameter = ParameterPart[1];
            string[] Arguments = Parameter.Split(',').Select(o => o.TrimStart()).ToArray();
            for(int i = 0; i < Arguments.Length; i++)
            {
                string[] ArgumentsSplits = Arguments[i].Split(' ');
                if (IsValidType(ArgumentsSplits[0]) && IsValidVariableName(ArgumentsSplits[1]))
                    FunctionModel.ArgumentVariables.Add(new VariableModel(ArgumentsSplits[0], ArgumentsSplits[1]));
                else
                {
                    FunctionModel.IsValid = false;
                    break;
                }
            }
            return FunctionModel;
        }
        
        public static bool IsProperStart(string name)
        {
            char cccc = name[0];
            if  (CharIsCaps(cccc) || CharIsSmall(cccc) || CharIsUnderscore(cccc))
                return true;
            return false;
        }
        public static bool GotProperFlow(string name)
        {
            bool FLAG = false;
            for(int i = 1; i < name.Length; i++)
            {
                char cccc = name[i];
                if (CharIsNumber(cccc) || CharIsCaps(cccc) || CharIsSmall(cccc) || CharIsUnderscore(cccc))
                    FLAG = true;
                else
                {
                    FLAG = false;
                    break;
                }   
            }
            return FLAG;
        }

        public static bool CharIsNumber(char c)
        {
            return c >= 48 && c <= 57;
        }
        public static bool CharIsCaps(char c)
        {
            return c >= 65 && c <= 90;
        }
        public static bool CharIsSmall(char c)
        {
            return c >= 97 && c <= 122;
        }
        public static bool CharIsUnderscore(char c)
        {
            return c == 95;
        }

        public static bool IsValidVariableName(string varName)
        {
            return IsProperStart(varName) && GotProperFlow(varName);
        }
        public static bool IsValidType(string type)
        {
            string[] ReturnTypes = new string[] {"void", "int", "float", "string", "char", "decimal", "double", "long" };
            return ReturnTypes.Contains(type);
        }
        public static bool IsValidAM(string accessModifier)
        {
            string[] AccessMod = new string[] { "public", "private" };
            return AccessMod.Contains(accessModifier);
        }
    }

    public class MethodModel
    {
        public string AccessModifier;
        public string ReturnType;
        public string MethodName;
        public bool IsValid = true;
        public List<VariableModel> ArgumentVariables = new List<VariableModel>();
    }
    public class VariableModel
    {
        public string Type;
        public string Name;
        public VariableModel(string type, string name)
        {
            this.Type = type;
            this.Name = name;
        }
        public VariableModel()
        {

        }
    }
}
