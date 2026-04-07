/*Colegio Técnico Antônio Teixeira Fernandes (Univap)
* Curso Técnico em Informática - Data de Entrega: 08 / 04 / 2026
* Autores do Projeto: Heitor Pinheiro de Souza
*                     Mateus Todeschini Monteiro
*
* Turma: 3I
* O Decágono Estocástico via Amostragem de Pontos.
* Observação: < colocar se houver>
* 
* 
* ******************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoICG1BI
{
	public partial class Form1 : Form
	{
        int r = 0, g = 0, b = 0;
		bool bntapertado = false;
		public Form1()
		{
			InitializeComponent();

		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
		public Color cores(int r, int g, int b)
		{
			Color cor = new Color();
			cor = Color.FromArgb(r, g, b);
			return cor;
		}
		public Pen caneta(Color cor)
		{
			Pen caneta = new Pen(cor);
			return caneta;
		}


		public void pintaP(Pen caneta, int x, int y, PaintEventArgs e)
		{
			e.Graphics.DrawLine(caneta, x, y, x + 1, y);
		}
		public float Modulo(float a, float b)
		{
			return (a % b + b) % b;
		}
		public int delta(int p, int centro)
		{
			return p - centro;
		}
		public double distance (int dx, int dy)
		{
			return Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
		}
		public double angulo(int dx, int dy)
		{
			return Math.Atan2(dy, dx);
		}
		public double seccao(int lados)
		{
			return (2*Math.PI)/lados;
		}
		public double distancemax (int Raio, int lados, float a) 
		{
			return Raio * (Math.Cos(Math.PI / lados) / Math.Cos(a));
		}
		public void decagono(PaintEventArgs e, int r, int g, int b)
		{
			int n = 10; // Lados
			int R = 150; // Raio externo
			int tentativas = 80000;
			int largura = 800;
			int altura = 800;
			int centroX = largura / 2;
			int centroY = altura / 2;
			Random rnd = new Random();
			for (int i = 0; i < tentativas; i++)
			{

				// 1. Gera ponto aleatório no "bounding box" do decágono
				int px = rnd.Next(centroX - R, centroX + R);
				int py = rnd.Next(centroY - R, centroY + R);
				int dx = delta(px, centroX);
				int dy = delta(py, centroY);
				double d = distance(dx, dy);
				double ang = angulo(dx, dy);
				double secc = seccao(n);
				float a = Modulo((float)ang, (float)secc) - ((float)secc/ 2);
				double d_max = distancemax(R, n, a);
				if (d < d_max)
				{
					pintaP(caneta(cores(r, g, b)), px, py, e);
				}

			}
		}
		private void Form1_Paint(object sender, PaintEventArgs e)
		{
            if (bntapertado)
            {
                Color cor = corDoComboBox();
                decagono(e, r, g, b);
                bntapertado = false;
            }
        }

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
        public Color corDoComboBox()
		{
			if (comboBox1.SelectedItem == null)
			{
                return cores(255, 0, 0);
            }
            string CorSelecionada = comboBox1.SelectedItem.ToString();
			switch (CorSelecionada)
			{
				case "Vermelho": r = 255; g = 0; b = 0; break;
				case "Verde": r = 0; g = 255; b = 0; break;
				case "Azul": r = 0; g = 0; b = 255; break;
				case "Amarelo": r = 255; g = 255; b = 0; break;
				case "Ciano": r = 0; g = 255; b = 255; break;
				case "Magenta": r = 255; g = 0; b = 255; break;
				case "Preto": r = 0; g = 0; b = 0; break;
                default: break;
			}

			return cores(r, g, b);
		}
        private void button1_Click(object sender, EventArgs e)
		{
            if (comboBox1.SelectedItem == null) {
				MessageBox.Show("Selecione uma cor antes de desenhar o decágono.");
				return; 
			}

            bntapertado = true;
			this.Invalidate();

		}
	}
}
