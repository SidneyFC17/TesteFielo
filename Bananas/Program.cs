using System;
using System.Collections.Generic;

namespace Bananas
{
    class Program
    {
        public static int[,] matriz;
        public static Dictionary<String, List<Coordenada>> mapPossiveisCaminhos;

        public static List<Caminho> lstCaminho;

        public static int linhas;
        public static int colunas;

        public class Caminho: IComparable<Caminho>
        {
            public String caminho { get; set; }
            public int valor { get; set; }

            public Caminho(String cam, int val)
            {
                caminho = cam;
                valor = val;
            }

            public override string ToString()
            {
                return caminho+ " = " + valor;
            }

            public int CompareTo(Caminho other)
            {
                if (this.valor==other.valor)
                {
                    return 0;
                } else if (this.valor > other.valor)
                {
                    return -1;
                }
                return 1;
            }
        }


        public class Coordenada
        {
            public int linha { get; set; }
            public int coluna { get; set; }

            public Coordenada(int l, int c)
            {
                linha = l;
                coluna = c;
            }

            public override string ToString()
            {
                return (linha + "," + coluna);
            }
        }

        public static void Main(string[] args)
        {
            lstCaminho = new List<Caminho>();
            linhas = 0;
            colunas = 0;
            Console.WriteLine("Quantas linhas?");
            linhas = int.Parse(Console.ReadLine());

            Console.WriteLine("Quantas colunas?");
            colunas = int.Parse(Console.ReadLine());

            matriz = new int[linhas, colunas];

            for (int l = 0; l < linhas; l++)
            {
                for (int c = 0; c < colunas; c++)
                {
                    Console.WriteLine("Digite o valor da posição (" + l + "," + c + ")");
                    matriz[l, c] = int.Parse(Console.ReadLine());
                }
            }

            mapPossiveisCaminhos = new Dictionary<String, List<Coordenada>>();

            for (int l = 0; l < linhas; l++)
            {
                for (int c = 0; c < colunas - 1; c++)
                {
                    String chave = l + "," + c;
                    if (!mapPossiveisCaminhos.ContainsKey(chave))
                    {
                        mapPossiveisCaminhos.Add(chave, new List<Coordenada>());
                    }

                    List<Coordenada> lstTemp;

                    mapPossiveisCaminhos.TryGetValue(chave, out lstTemp);
                    lstTemp.Add(new Coordenada(l, (c+1)));
                    mapPossiveisCaminhos[chave] = lstTemp;

                    if (l > 0)
                    {
                        mapPossiveisCaminhos.TryGetValue(chave, out lstTemp);
                        lstTemp.Add(new Coordenada((l - 1), (c + 1)));
                        mapPossiveisCaminhos[chave] = lstTemp;
                    }

                    if (l < (linhas - 1))
                    {
                        mapPossiveisCaminhos.TryGetValue(chave, out lstTemp);
                        lstTemp.Add(new Coordenada((l + 1), (c + 1)));
                        mapPossiveisCaminhos[chave] = lstTemp;
                    }
                }
            }

            Console.Clear();
            Console.WriteLine("Estrutura: ");
            for (int l = 0; l < linhas; l++){
                for (int c = 0; c < colunas; c++){
                    Console.Write("|" + matriz[l,c]+ "\t|");
                }
                Console.WriteLine();
            }
            
            for (int l = 0; l < linhas; l++)
            {
                Coordenada cd = new Coordenada(l, 0);
                exibirCaminhosPossiveis(cd, cd.ToString(), matriz[l,0]);
            }

            lstCaminho.Sort();

            Console.WriteLine("\nMelhore(s) caminho(s):");
            int melhorRes;
            int indice = 0;
            do
            {
                melhorRes = lstCaminho[indice].valor;
                Console.WriteLine(lstCaminho[indice].caminho + "=" + lstCaminho[indice].valor);
                indice++;
            } while (lstCaminho[indice].valor == melhorRes);



            Console.WriteLine("\n\n\nAperte [ENTER] para finalizar.");
            Console.ReadLine();
        }

        public static void exibirCaminhosPossiveis(Coordenada cd, string caminho, int valor)
        {
            List<Coordenada> lstTemp;
            mapPossiveisCaminhos.TryGetValue(cd.ToString(), out lstTemp);

            if (lstTemp != null)
            {
                foreach (Coordenada cds in lstTemp)
                {
                    String novoCaminho = caminho + "->" + cds.ToString();
                    int novoValor = valor + matriz[cds.linha, cds.coluna];
                    if (cds.coluna == (colunas-1)) {
                        lstCaminho.Add(new Caminho(novoCaminho, novoValor));
                        
                        Console.WriteLine(novoCaminho + ", " + novoValor);
                        
                        continue;
                    }
                    exibirCaminhosPossiveis(cds,novoCaminho,novoValor);
                }
            }
            return;

        }
    }
}
