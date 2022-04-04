namespace CoreApp
{
    public enum TipoVar
    {
        Numerico,
        Texto,
        Booleano,
        Fecha,
        Hora
    }

    public class Misc
    {

        public static string ObjToString(object InputValue)
        {
            return InputValue == null || InputValue == DBNull.Value ? null : (string)InputValue;
        }

        public static DateTime? ObjToDateTime(object InputValue)
        {
            DateTime? FechaNula = null;
            return InputValue == null || InputValue == DBNull.Value ? FechaNula : (DateTime)InputValue;
        }

        public static int? ObjToInt(object InputValue)
        {
            int? IntNulo = null;
            return InputValue == null || InputValue == DBNull.Value ? IntNulo : Convert.ToInt32(InputValue); 
        }

        public static Byte? ObjToByte(object InputValue)
        {
            Byte? ByteNulo = null;
            return InputValue == null || InputValue == DBNull.Value ? ByteNulo : (Byte)InputValue;
        }

        public static Boolean? ObjToBoolean(object InputValue)
        {
            Boolean? BooleanNulo = null;
            return InputValue == null || InputValue == DBNull.Value ? BooleanNulo : (Boolean)InputValue;
        }

        public static string VarSQL(string Valor, TipoVar Tipo, bool CeroNulo = false, bool EliminarComodinesSQL = true)
        {
            //Devuelve un valor de variable en formato de SQL Server
            //Usado para componer cadenas de argumentos para SP        

            if (string.IsNullOrEmpty(Valor)) { return "NULL"; }
            if (Valor.Length == 0) { return "NULL"; }
            if (Valor == "0:00:00") { return "NULL"; }

            //Prevenir cadenas de SQL Injection
            if (EliminarComodinesSQL)
            {
                Valor.Replace("SELECT", "");
                Valor.Replace("UPDATE", "");
                Valor.Replace("DELETE", "");
                Valor.Replace("DROP", "");
                Valor.Replace("UNION", "");
                Valor.Replace("%", "");
                Valor.Replace(" TOP ", "");
                Valor.Replace(" GROUP ", "");
                Valor.Replace("=", "");
                Valor.Replace(">", "");
                Valor.Replace("<", "");
                Valor.Replace("IIF", "");
                Valor.Replace("FROM", "");
                Valor.Replace(" OR ", "");
                Valor.Replace(" AND ", "");
                Valor.Replace(" IN ", "");
                Valor.Replace(" CHR ", "");
                Valor.Replace(" ASC(", "");
                Valor.Replace(" CurDir", "");
                Valor.Replace("LEN(", "");
                Valor.Replace("SHELL", "");
                Valor.Replace("ASCII", "");
                Valor.Replace("SUBSTRING", "");
                Valor.Replace("LENGHT", "");
                Valor.Replace("exists", "");
            }

            string valRet;

            switch (Tipo)
            {
                case TipoVar.Numerico:
                    valRet = Valor.Replace(",", ".");
                    break;

                case TipoVar.Texto:
                case TipoVar.Hora:
                    valRet = "'" + Valor.Replace("'", "''") + "'";
                    break;

                case TipoVar.Booleano:
                    valRet = (Valor == "True") ? "1" : "0";
                    break;

                case TipoVar.Fecha:
                    valRet = "'" + string.Format("{0:yyyMMdd}", Convert.ToDateTime(Valor)) + "'";
                    break;

                default:
                    valRet = "NULL";
                    break;
            }

            if (valRet == "0" && CeroNulo) { valRet = "NULL"; }
            if (valRet == "'0'" && CeroNulo) { valRet = "NULL"; }

            return valRet;
        }

        public static string VarSQL(int Valor, TipoVar Tipo, bool CeroNulo = false, bool EliminarComodinesSQL = true)
        {
            return VarSQL(Valor.ToString(), Tipo, CeroNulo, EliminarComodinesSQL);
        }

        public static string VarSQL(int? Valor, TipoVar Tipo, bool CeroNulo = false, bool EliminarComodinesSQL = true)
        {
            return VarSQL(Valor.ToString(), Tipo, CeroNulo, EliminarComodinesSQL);
        }


        public static string VarSQL(DateTime Valor, TipoVar Tipo, bool CeroNulo = false, bool EliminarComodinesSQL = true)
        {
            return VarSQL(Valor.ToString(), Tipo, CeroNulo, EliminarComodinesSQL);
        }

        public static string VarSQL(DateTime? Valor, TipoVar Tipo, bool CeroNulo = false, bool EliminarComodinesSQL = true)
        {
            return VarSQL(Valor.ToString(), Tipo, CeroNulo, EliminarComodinesSQL);
        }


        public static string VarSQL(Boolean Valor, TipoVar Tipo, bool CeroNulo = false, bool EliminarComodinesSQL = true)
        {
            return VarSQL(Valor.ToString(), Tipo, CeroNulo, EliminarComodinesSQL);
        }

        public static string VarSQL(Boolean? Valor, TipoVar Tipo, bool CeroNulo = false, bool EliminarComodinesSQL = true)
        {
            return VarSQL(Valor.ToString(), Tipo, CeroNulo, EliminarComodinesSQL);
        }
    }
}
