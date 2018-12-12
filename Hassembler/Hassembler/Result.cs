using System;

namespace Hassembler
{
    class Result
    {

        enum Type {Int, Bool, Float, String, Char, Undefined};

        private Type result_type = Type.Undefined;

        private int int_result;
        public int Int_result
        {
            get
            {
                if (result_type != Type.Int)
                    throw new Exception("Ei oo int");
                else return int_result;
            }
        }
        private bool bool_result;
        public bool Bool_result
        {
            get
            {
                if (result_type != Type.Bool)
                    throw new Exception("Ei oo bool");
                else return bool_result;
            }
        }
        private float float_result;
        public float Float_result
        {
            get
            {
                if (result_type != Type.Float)
                    throw new Exception("Ei oo float");
                else return float_result;
            }
        }
        private string string_result;
        public string String_result
        {
            get
            {
                if (result_type != Type.String)
                    throw new Exception("Ei oo string");
                else return string_result;
            }
        }
        private char char_result;
        public int Char_result
        {
            get
            {
                if (result_type != Type.Char)
                    throw new Exception("Ei oo char");
                else return char_result;
            }
        }

        public Result(int value)
        {
            result_type = Type.Int;
            int_result = value;
        }

        public Result(bool value)
        {
            result_type = Type.Bool;
            bool_result = value;
        }

        public Result(float value)
        {
            result_type = Type.Float;
            float_result = value;
        }

        public Result(string value)
        {
            result_type = Type.String;
            string_result = value;
        }

        public Result(char value)
        {
            result_type = Type.Char;
            char_result = value;
        }

    }
}