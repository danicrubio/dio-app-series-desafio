namespace DIO.Series
{
  class Program
  {
    static SerieRepositorio repositorio = new SerieRepositorio();
    static void Main(string[] args)
    {
      string opcaoUsuario = ObterOpcaoUsuario();

      while (opcaoUsuario.ToUpper() != "X")
      {
        switch (opcaoUsuario)
        {
          case "1":
            ListarSeries();
            break;
          case "2":
            repositorio.Insere(CadastroSerie(opcaoUsuario, 0));
            break;
          case "3":
            Console.Write("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());
            repositorio.Atualiza(indiceSerie, CadastroSerie(opcaoUsuario, indiceSerie));
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
        opcaoUsuario = ObterOpcaoUsuario();
      }
      Console.WriteLine("Obrigado por utilizar nossos serviços.");
      Console.WriteLine("Digite enter para finalizar...");
      Console.ReadLine();
    }

    private static void VisualizarSerie()
    {
      Console.WriteLine("Digite o id da série: ");
      int indiceSerie = int.Parse(Console.ReadLine());

      var serie = repositorio.RetornaPorId(indiceSerie);

      Console.WriteLine(serie);
    }

    private static void ExcluirSerie()
    {
      Console.WriteLine("Digite o id da série: ");
      int indiceSerie = int.Parse(Console.ReadLine());

      Console.WriteLine($"Quer mesmo excluir essa série?");
      Console.WriteLine($"Digite Sim para excluí-la ou não para cancelar");

      var decisaoUsuario = Console.ReadLine();

      if (decisaoUsuario.ToUpper() == "SIM")
      {
        repositorio.Exclui(indiceSerie);
      }
    }

    private static Serie CadastroSerie(string opcaoUsuario, int indiceSerie)
    {
      foreach (int i in Enum.GetValues(typeof(Genero)))
      {
        Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
      }
      Console.WriteLine("Digite o gênero entre as opções acima: ");
      int entradaGenero = int.Parse(Console.ReadLine());

      Console.WriteLine("Digite o Título da Série: ");
      string entradaTitulo = Console.ReadLine();

      Console.Write("Digite o Ano de Início da Série: ");
      int entradaAno = int.Parse(Console.ReadLine());

      Console.WriteLine("Digite a Descrição da Série: ");
      string entradaDescricao = Console.ReadLine();

      int indice;

      if (opcaoUsuario == "2")
      {
        indice = repositorio.ProximoId();
      }
      else
      {
        indice = indiceSerie;
      }

      Serie serie = new Serie(id: indice,
                                  genero: (Genero)entradaGenero,
                                  titulo: entradaTitulo,
                                  ano: entradaAno,
                                  descricao: entradaDescricao);
      return serie;
    }

    private static void ListarSeries()
    {
      Console.WriteLine("Listar séries");

      var lista = repositorio.Lista();

      if (lista.Count == 0)
      {
        Console.WriteLine("Nenhuma série cadastrada.");
        return;
      }

      foreach (var serie in lista)
      {
        var excluido = serie.retornaExcluido();

        Console.WriteLine("#ID {0}: - {1} - {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "Excluído" : ""));
      }
    }

    private static string ObterOpcaoUsuario()
    {
      Console.WriteLine();
      Console.WriteLine("DIO Séries a seu dispor!!!");
      Console.WriteLine("Informe a opção desejada:");

      Console.WriteLine("1- Listar séries");
      Console.WriteLine("2- Inserir nova série");
      Console.WriteLine("3- Atualizar série");
      Console.WriteLine("4- Excluir série");
      Console.WriteLine("5- Visualizar série");
      Console.WriteLine("C- Limpar Tela");
      Console.WriteLine("X- Sair");
      Console.WriteLine();

      string opcaoUsuario = Console.ReadLine().ToUpper();
      Console.WriteLine();
      return opcaoUsuario;
    }
  }
}
