using FoolProof.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CiaTecnica.DTO
{
    public class ClienteDTO
    {
        public ClienteDTO() { }

        public ClienteDTO(string cnpj_cpf, bool isPessoaFisica, DateTime dataNascimento, string nome_razaoSocial, string nomeFantasia, string sobrenome,
                       string cep, string logradouro, string numero, string completemento, string bairro, string cidade, string uf)
        {
            this.Cnpj_Cpf = cnpj_cpf;
            this.IsPessoaFisica = isPessoaFisica;
            this.DataNascimento = dataNascimento;
            this.Nome_RazaoSocial = nome_razaoSocial;
            this.NomeFantasia = nomeFantasia;
            this.Sobrenome = sobrenome;
            this.Cep = cep;
            this.Logradouro = logradouro;
            this.Numero = numero;
            this.Completemento = completemento;
            this.Bairro = bairro;
            this.Cidade = cidade;
            this.Uf = uf; 
        }

        [Required]
        public int ClienteId { get; set; }
        
        [Required(ErrorMessage ="Campo obrigatório.")]
        [Display(Name = "CPF / CNPJ")]
        public string Cnpj_Cpf { get; set; }
        
        [Required]
        public bool IsPessoaFisica { get; set; }
        
        [RequiredIfTrue("IsPessoaFisica", ErrorMessage = "Informe a data de nascimento.")]
        [Display(Name = "Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }
        
        [Required(ErrorMessage ="Campo obrigatório.")]
        [Display(Name = "Nome / Razão Social")]
        public string Nome_RazaoSocial { get; set; }

        [RequiredIfFalse("IsPessoaFisica", ErrorMessage = "Informe o Nome Fantasia")]
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [RequiredIfTrue("IsPessoaFisica", ErrorMessage = "Informe o Sobrenome.")]
        [MaxLength(15, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [Display(Name = "Sobrenome")]
        public string Sobrenome { get; set; }
        
        [Required(ErrorMessage = "Informe o CEP")]
        [MaxLength(9, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string Cep { get; set; }
        
        [Required(ErrorMessage ="Informe o logradouro")]
        public string Logradouro { get; set; }
        
        [Required(ErrorMessage ="Informe o número.")]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        public string Completemento { get; set; }
        
        [Required(ErrorMessage ="Informe o Bairro.")]
        public string Bairro { get; set; }
        
        [Required(ErrorMessage ="Informe a Cidade.")]
        public string Cidade { get; set; }
        
        [Required(ErrorMessage ="Informe a UF.")]
        public string Uf { get; set; } //(sigla)
    }
}

