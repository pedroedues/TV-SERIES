using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string option = ObterOpcaoUsuario();
            while(option.ToUpper() != "X")
            {
                switch (option)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            option = ObterOpcaoUsuario();
            } 
        }

        private static void ListarSeries()
        {
            Console.WriteLine("\n Listagem de séries");
            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.Write("Não foi possível identificar nenhuma série. Volte ao menu e faça os devidos cadastros");
                return;
            }

            foreach (var serie in lista)
                Console.WriteLine("ID {0}: {1}", serie.retornaId(), serie.retornaTitulo());

        }

        private static void InserirSerie()
        {
            Console.WriteLine("\n\n Inserir nova série");
            Console.WriteLine("\n Qual o gênero da série?");
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            object serieChange = AddOrUpdateSerie(repositorio.ProximoId());
            repositorio.Insere((Serie) serieChange);
        }

        private static void AtualizarSerie()
        {
            Console.WriteLine("Atualizar série");
            ListarSeries();
            Console.Write("Digite o ID da série que deseja atualizar:");
            int indiceSerie = int.Parse(Console.ReadLine());
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            object serieChange = AddOrUpdateSerie(indiceSerie);
            repositorio.Atualiza(indiceSerie, (Serie) serieChange);
        }

        private static object AddOrUpdateSerie(int id)
        {
            Console.Write("Digite o número do gênero correspondente: ");
            int generoOption = int.Parse(Console.ReadLine());
            Console.Write("Título: ");
            string title = Console.ReadLine();
            Console.Write("Digite o ano de início da série: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Descreva a série: ");
            string description = Console.ReadLine();

            Serie serieChange = new Serie(id: id,
                                        genero: (Genero)generoOption,
                                        titulo: title,
                                        ano: year,
                                        descricao: description);
            return serieChange;
        }

        private static void ExcluirSerie()
        {
            Console.WriteLine("Exclusão de série");
            ListarSeries();
            Console.Write("Digite o ID da série que deseja excluir: ");
            int indiceSerie = int.Parse(Console.ReadLine());
            Console.WriteLine("Tem certeza que deseja excluir essa série? (S/N) ");
            string option = Console.ReadLine().ToUpper();
            if (option == "S") repositorio.Exclui(indiceSerie);
        }

        private static void VisualizarSerie()
        {
            Console.WriteLine("Visualização completa de série");
            ListarSeries();
            Console.Write("Digite o ID da série que deseja visualizar: ");
            int indiceSerie = int.Parse(Console.ReadLine());
            var serie = repositorio.RetornaPorId(indiceSerie);
            Console.WriteLine(serie);
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("Informe a opçãp desejada:"); 
            Console.WriteLine("1 - Listar séries");
            Console.WriteLine("2 - Inserir nova série");
            Console.WriteLine("3 - Atualizar série");
            Console.WriteLine("4 - Excluir série");
            Console.WriteLine("5 - Visualizar série");
            Console.WriteLine("C - Limpar tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }


    }
}
