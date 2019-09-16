using FluentQuery;

namespace FluenteQuery.ConsoleSample
{
    public struct PessoaEntity
    {
        private string EntityName => "Pessoa";
        public  EntityField PessoaId => new EntityField(EntityName, "PessoaId");
        public  EntityField Nome => new EntityField(EntityName, "Nome");
        public  EntityField Idade => new EntityField(EntityName, "Idade");
        public  EntityField Sexo => new EntityField(EntityName, "Sexo");
        public  EntityField Telefone => new EntityField(EntityName, "Telefone");
        public  EntityField EnderecoId => new EntityField(EntityName, "EnderecoId");
        public override string ToString() => EntityName;
    }
    
    public struct UfEntity
    {
        private string EntityName => "Uf";
        public  EntityField UfId => new EntityField(EntityName, "UfId");
        public  EntityField Nome => new EntityField(EntityName, "Nome");
        public  EntityField Sigla => new EntityField(EntityName, "Sigla");
        public override string ToString() => EntityName;
    }
    
    public struct CidadeEntity
    {
        private string EntityName => "Cidade";
        public  EntityField CidadeId => new EntityField(EntityName, "CidadeId");
        public  EntityField Nome => new EntityField(EntityName, "Nome");
        public  EntityField UfId => new EntityField(EntityName, "UfId");
        public override string ToString() => EntityName;
    }
    
    public struct EnderecoEntity
    {
        private string EntityName => "Endereco";
        public  EntityField EnderecoId => new EntityField(EntityName, "EnderecoId");
        public  EntityField Situacao => new EntityField(EntityName, "Situacao");
        public  EntityField Descricao => new EntityField(EntityName, "Descricao");
        public  EntityField CidadeId => new EntityField(EntityName, "CidadeId");
        public override string ToString() => EntityName;
    }
    
    public struct FuncionarioEntity
    {
        private string EntityName => "Funcionario";
        public EntityField PessoaId => new EntityField(EntityName, "PessoaId");
        public EntityField Salario => new EntityField(EntityName, "Salario");
        public EntityField Funcao => new EntityField(EntityName, "Funcao");
        public override string ToString() => EntityName;
    }
    
    public struct Entities
    {
        public static PessoaEntity Pessoa => new PessoaEntity();
        public static UfEntity Uf => new UfEntity();
        public static CidadeEntity Cidade => new CidadeEntity();
        public static EnderecoEntity Endereco => new EnderecoEntity();
        public static FuncionarioEntity Funcionario => new FuncionarioEntity();
    }
}