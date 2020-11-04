using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BananasTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Teste()
        {
            Bananas.Program.matriz = new int[3,3];
            Bananas.Program.matriz[0, 0] = 1;
            Bananas.Program.matriz[0, 1] = 3;
            Bananas.Program.matriz[0, 2] = 3;
            Bananas.Program.matriz[1, 0] = 2;
            Bananas.Program.matriz[1, 1] = 1;
            Bananas.Program.matriz[1, 2] = 4;
            Bananas.Program.matriz[2, 0] = 0;
            Bananas.Program.matriz[2, 1] = 6;
            Bananas.Program.matriz[2, 2] = 4;

            Bananas.Program.colunas = 3;
            Bananas.Program.linhas = 3;
            Bananas.Program.lstCaminho = new List<Bananas.Program.Caminho>();

            Bananas.Program.popularMapaPossiveisCaminhos();

            for (int l = 0; l < Bananas.Program.linhas; l++)
            {
                Bananas.Program.Coordenada cd = new Bananas.Program.Coordenada(l, 0);
                Bananas.Program.gravarCaminhosPossiveis(cd, cd.ToString(), Bananas.Program.matriz[l, 0]);
            }

            Bananas.Program.lstCaminho.Sort();

            int valorMax = Bananas.Program.lstCaminho[0].valor;

            Assert.AreEqual(12,valorMax);
        }
    }
}
