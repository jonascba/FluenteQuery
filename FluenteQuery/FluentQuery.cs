using System;

namespace FluentQuery
{
    public class EntityField
    {
        private readonly string _fieldName;
        private readonly string _entityName;
        private readonly string _options;
        public override string ToString() => $" {_entityName}.{_fieldName} {_options}";
        
        public EntityField(string entityName, string fieldName) => (_entityName, _fieldName) = (entityName, fieldName);
        public EntityField(string entityName, string fieldName, string options) => (_entityName, _fieldName, _options) = (entityName, fieldName, options);
        
        public string In (params string[] values ) => $"( {_entityName}.{_fieldName} IN ({ GetProjection(",", values) }) )";
        public string In (params int[] values ) => $"( {_entityName}.{_fieldName} IN ({ GetProjection(string.Empty, values) }) )";
        
        public string In ( string statement ) => $"( {_entityName}.{_fieldName} IN '({statement})' )";
        
        public string Like (string text) => $"( {_entityName}.{_fieldName} LIKE '{text}' )";
        public EntityField Desc() => new EntityField(_entityName, _fieldName, "DESC");
        public static string operator *(EntityField f1, EntityField f2) => $"( {f1} + {f2} )";
        public static string operator *(EntityField f1, double f2) => $"( {f1} * {f2} )";
        public static string operator *(EntityField f1, string f2) => $"( {f1} * {f2} )";
        public static string operator *(string f1, EntityField f2) => $"( {f1} + {f2} )";
        public static string operator +(EntityField f1, EntityField f2) => $"( {f1} + {f2} )";
        public static string operator ==(EntityField f1, EntityField f2) => $"( {f1} = {f2} )";
        public static string operator ==(EntityField f1, string f2) => $"( {f1} = '{f2}' )";
        public static string operator !=(EntityField f1, string f2) => $" ( {f1} <> '{f2}' )";
        public static string operator ==(EntityField f1, int f2) => $"( {f1} = {f2} )";
        public static string operator !=(EntityField f1, int f2) => $" ( {f1} <> {f2} )";
        public static string operator !=(EntityField f1, EntityField f2) => $" ( {f1} <> {f2} )";
        public static string operator >(EntityField f1, int f2) => $" ( {f1} > {f2} )";
        public static string operator <(EntityField f1, int f2) => $" ( {f1} < {f2} )";
        public static string operator >=(EntityField f1, int f2) => $" ( {f1} >= {f2} )";
        public static string operator <=(EntityField f1, int f2) => $" ( {f1} <= {f2} )";
        public static string operator >(EntityField f1, double f2) => $" ( {f1} > {f2} )";
        public static string operator <(EntityField f1, double f2) => $" ( {f1} < {f2} )";
        public static string operator >=(EntityField f1, double f2) => $" ( {f1} >= {f2} )";
        public static string operator <=(EntityField f1, double f2) => $" ( {f1} <= {f2} )";
        private string GetProjection<T>(string delimit = "", params T[] values)
        {
            var valuesString = "";
            foreach (var value in values){
                if (valuesString.Length > 0) valuesString += ",";
                valuesString += $"{delimit}{value}{delimit}";
            }
            return valuesString;
        }
    }
 
 
    public class FluentQuery
    {
        private string _entity;
        private String sqlStmt = "";
        public override string ToString() => sqlStmt;
        
        private string GetCommaSeparetedList<T>(string delimit = "", params T[] values)
        {
            var valuesString = "";
            foreach (var value in values){
                if (valuesString.Length > 0) valuesString += ",";
                valuesString += $"{delimit}{value}{delimit}";
            }
            return valuesString;
        }
        private FluentQuery Self(Action action)
        {
            action();
            return this;
        }
        private FluentQuery SetEntity(object entityName) => Self( () => _entity = entityName.ToString());
        
        private void Add(string instruction) => sqlStmt += instruction ;
        private FluentQuery Instruction(string instruction) => Self(() => Add( instruction ) );
        
        public FluentQuery Select(params EntityField[] fields) => Instruction($" SELECT {GetCommaSeparetedList(String.Empty, fields)} \r\n");
        
        public FluentQuery Select(params object[] fields) => Instruction($" SELECT {GetCommaSeparetedList(String.Empty, fields)} \r\n");
        
        public FluentQuery Also() => Instruction( $" ," );
        
        public FluentQuery Also(params EntityField[] fields) => Instruction($" ,{GetCommaSeparetedList(String.Empty, fields)} \r\n");
        public FluentQuery Also(params object[] fields) => Instruction($" ,{GetCommaSeparetedList(String.Empty, fields)} \r\n");
        
        public FluentQuery 〡a => Instruction( $" ," );
        
        public FluentQuery Expr(object expression) => Instruction($" {expression} ");
        
        public FluentQuery 〡(object expression) => Instruction($" ,{expression} ");
        public FluentQuery 〡〡(object expression) => Instruction($" {expression} ");
        
        public FluentQuery Desc => Instruction($" DESC ");
        
        public FluentQuery Include(object expression) => Instruction($" {expression} ");
        
        public FluentQuery Count => Instruction($" COUNT(*) "); 
        
        public FluentQuery 〡Count => Instruction($" COUNT(*) ");
        
        public FluentQuery Sum(object expression) => Instruction($" SUM({expression}) ");
        
        public FluentQuery 〡Sum(object expression) => Instruction($" \r\n,SUM({expression}) ");
        
        public FluentQuery 〡Σ(object expression) => Instruction($" \r\n,SUM({expression}) ");
        
        
        public FluentQuery IsNull(object checkExpression, object replacementValue) => Instruction($" ISNULL({checkExpression}, {replacementValue}) ");

        public FluentQuery IsNull(object checkExpression, string replacementValue) => Instruction($" ISNULL({checkExpression}, '{replacementValue}') ");

        public FluentQuery Max(object expression) => Instruction($" MAX({expression}) ");
        
        public FluentQuery From(object entity) => 
            SetEntity(entity)
                .Instruction($"  FROM {entity} \r\n");

        public FluentQuery LeftJoin(object entity) => Instruction($"    LEFT JOIN {entity} ");
        
        public FluentQuery CrossApply(string expression) => Instruction($"    CROSS APPLY ( {expression} ) \r\n ");
        
        public FluentQuery As(string alias) =>Instruction($" AS { alias } ");
        
        public FluentQuery InnerJoin(object entity) => Instruction($"    INNER JOIN {entity} ");
        
        public FluentQuery On(string expression) => Instruction($" ON {expression} \r\n");
        
        public FluentQuery On〱〱(string expression) => Instruction($" ON ({expression} \r\n");
        
        public FluentQuery OnPredicate(string expression) => Instruction($" ON ({expression} \r\n");
        
        public FluentQuery Simbol(string simbol) => Instruction($" {simbol} ");
        
        public FluentQuery Where(string expression) => Instruction($"  WHERE {expression} \r\n");

        public FluentQuery And(string expression) => Instruction($"     AND {expression} \r\n");

        public FluentQuery And〱〱(string expression) => Instruction($"     AND ({expression} \r\n");

        public FluentQuery 〱〱 => Instruction($"     )");
        
        public FluentQuery Or(string expression) => Instruction($"     OR {expression} \r\n");
        
        public FluentQuery 〡〡(string expression) => Instruction($"     OR {expression} \r\n");
        
        public FluentQuery AsJson() => this;
        
        public FluentQuery GroupBy(params EntityField[] fields) => 
            Instruction($" GROUP BY {GetCommaSeparetedList(String.Empty, fields)} \r\n");
        
        public FluentQuery OrderBy〱〱 => Instruction($"ORDER BY (");
       
        public FluentQuery OrderBy(params EntityField[] fields) => 
            Instruction($" ORDER BY {GetCommaSeparetedList(String.Empty, fields)} \r\n");

        public FluentQuery And(string simbol, string expression) => Instruction($"     AND {simbol}{expression} \r\n");
        public FluentQuery And() => Instruction($"     AND ");

        public FluentQuery Not() => Instruction($" NOT  ");
        public FluentQuery Exists(string expression) => Instruction($"     EXISTS ({expression}) ");

        public FluentQuery Limit(int rows) =>
            Self(() => sqlStmt = ReplaceFirst(sqlStmt, "SELECT ", $"SELECT TOP {rows} "));
        
        public string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

    }
}