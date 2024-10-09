using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BD3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection conexao;
            MySqlCommand cmd;
            conexao = new MySqlConnection("server=localhost;uid=root;pwd=;database=projeto");
            try
            {
                conexao.Open();
                Console.WriteLine("funcionando");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            string sql;
            sql = "select * from usuario";
            cmd = new MySqlCommand(sql, conexao);
            MySqlDataReader rdr;
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine("Id Usuario: " + rdr["id"] + "\n");
                Console.WriteLine("Usuario: " + rdr["usuario"] + "\nSenha: " + rdr["senha"] + "\n");
            }
            rdr.Close();


            //Inserir dados
            Console.WriteLine("inserir Dados\n");
            string usuario;
            string senha;

            Console.WriteLine("Digite um novo usuario\n");
            usuario = Console.ReadLine();

            Console.WriteLine("Digite a senha\n");
            senha = Console.ReadLine();

            sql = "inserir into usuario(usuario, senha) values (@usuario, @senha";
            cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@senha", senha);

            try
            {
                int LinhasaFetadas = cmd.ExecuteNonQuery();

                if (LinhasaFetadas == 0)
                {
                    throw new Exception("nenhuma linha foi afetada pela consulta.");

                }
                else
                {
                    Console.WriteLine("Linha afetadas:{0}", LinhasaFetadas);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro:{0}", ex.Message);
            }
            Console.ReadKey();
        }
    }
}