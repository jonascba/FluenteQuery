using System;
using static FluentQuery.Entities;
namespace FluentQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            var uf = "MT";
            
            var q = new FluentQuery();
            var query = q
                .Select(Pessoa.Nome)
                    .〡(Pessoa.Idade)
                    .〡(Funcionario.Salario * 200).As("Valor_Final")
                    .〡(Pessoa.Telefone).As("[Telefone Teste]")
                    .〡Sum(Funcionario.Salario).As("ValorSalario")
                .From(Funcionario)
                    .InnerJoin(Pessoa)  .On(Pessoa.PessoaId == Funcionario.PessoaId)
                    .LeftJoin(Endereco) .On〱〱(Endereco.EnderecoId == Pessoa.EnderecoId).And( Endereco.Situacao == "ATIVO").〱〱
                    .LeftJoin(Cidade)   .On(Cidade.CidadeId == Endereco.CidadeId)
                    .LeftJoin(Uf)       .On(Uf.UfId == Cidade.UfId)
                .Where(Funcionario.Salario >= 2500.95)
                    .And〱〱 (Pessoa.Idade > 18).Or(Pessoa.Sexo != "M").〱〱
                ;
            // Dynamic Where Condition
            if (uf != string.Empty)
                q = q.And(Uf.Sigla == uf);
                    
            q = q.OrderBy〱〱
                    .〡〡(Pessoa.Idade) .Desc
                    .〡(Pessoa.PessoaId)
                    .〱〱;
            
            Console.WriteLine(q);
        }
    }
}