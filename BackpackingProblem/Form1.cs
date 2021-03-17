using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BackpackingProblem
{
    public partial class Form1 : Form
    {
        private List<Przedmiot> przedmioty = new List<Przedmiot>();
        private List<Przedmiot> zapakowanePrzedmioty = new List<Przedmiot>();
        private int lacznaWartoscPlecaka = 0;
        private int lacznaWagaPlecaka = 0;

        private Panel daneLista;
        private TextBox nazwaPrzedmiotuTxt;
        private NumericUpDown wartoscPrzedmiotu;
        private NumericUpDown wagaPrzedmiotu;
        private DataGridView listaPrzedmiotow;
        
        private Panel plecak;
        private NumericUpDown rozmiarPlecaka;
        private DataGridView listaZapakowanychPrzedmiotow;
        private Label lacznaWarPlecTxt;
        private Label lacznaWagPlecTxt;
        
        public Form1() {
            InitializeComponent();
            stworzElementy();    
        }

        private void stworzElementy() {
            // panel lista przedmiotow + wprowadzenie danych
            daneLista = new Panel();
            daneLista.Location = new Point(10, 10);
            daneLista.Size = new Size(335, 480);
            daneLista.BackColor = Color.Green;
            this.Controls.Add(daneLista);

            // label wprowadz przedmiot
            Label wprowadzPrz = new Label();
            wprowadzPrz.Location = new Point(5, 5);
            wprowadzPrz.Size = new Size(240, 40);
            wprowadzPrz.Text = "Wprowadź przedmiot do listy";
            wprowadzPrz.Font = new Font(wprowadzPrz.Font.Name, 11F, wprowadzPrz.Font.Style, wprowadzPrz.Font.Unit);
            daneLista.Controls.Add(wprowadzPrz);

            // label nazwa przedmiotu
            Label nazwaPrz = new Label();
            nazwaPrz.Location = new Point(5, 45);
            nazwaPrz.Size = new Size(130, 40);
            nazwaPrz.Text = "Nazwa przedmiotu";
            nazwaPrz.Font = new Font(wprowadzPrz.Font.Name, 10F, wprowadzPrz.Font.Style, wprowadzPrz.Font.Unit);
            daneLista.Controls.Add(nazwaPrz);
            
            // textbox nazwa przedmiotu
            nazwaPrzedmiotuTxt = new TextBox();
            nazwaPrzedmiotuTxt.Location = new Point(135, 45);
            nazwaPrzedmiotuTxt.Size = new Size(195, 40);
            daneLista.Controls.Add(nazwaPrzedmiotuTxt);

            // label wartosc rzedmiotu
            Label wartoscPrz = new Label();
            wartoscPrz.Location = new Point(5, 85);
            wartoscPrz.Size = new Size(130, 40);
            wartoscPrz.Text = "Wartość";
            wartoscPrz.Font = new Font(wprowadzPrz.Font.Name, 10F, wprowadzPrz.Font.Style, wprowadzPrz.Font.Unit);
            daneLista.Controls.Add(wartoscPrz);

            // textbox wartosc przedmiotu
            wartoscPrzedmiotu = new NumericUpDown();
            wartoscPrzedmiotu.Location = new Point(135, 85);
            wartoscPrzedmiotu.Size = new Size(195, 40);
            wartoscPrzedmiotu.Maximum = 1000000;
            daneLista.Controls.Add(wartoscPrzedmiotu);

            // label waga rzedmiotu
            Label wagaPrz = new Label();
            wagaPrz.Location = new Point(5, 125);
            wagaPrz.Size = new Size(130, 40);
            wagaPrz.Text = "Waga";
            wagaPrz.Font = new Font(wprowadzPrz.Font.Name, 10F, wprowadzPrz.Font.Style, wprowadzPrz.Font.Unit);
            daneLista.Controls.Add(wagaPrz);
            
            // textbox waga przedmiotu
            wagaPrzedmiotu = new NumericUpDown();
            wagaPrzedmiotu.Location = new Point(135, 125);
            wagaPrzedmiotu.Size = new Size(195, 40);
            wagaPrzedmiotu.Maximum = 1000000;
            daneLista.Controls.Add(wagaPrzedmiotu);

            // button dodaj przedmiot
            Button dodajPrzedmiotButton = new Button();
            dodajPrzedmiotButton.Location = new Point(35, 170);
            dodajPrzedmiotButton.Size = new Size(125, 40);
            dodajPrzedmiotButton.Text = "Dodaj przedmiot";
            dodajPrzedmiotButton.Click += DodajPrzedmiot;
            daneLista.Controls.Add(dodajPrzedmiotButton);

            // button wyczysc liste przedmiotow
            Button wyczyscListeButton = new Button();
            wyczyscListeButton.Location = new Point(175, 170);
            wyczyscListeButton.Size = new Size(125, 40);
            wyczyscListeButton.Text = "Wyczyść listę";
            wyczyscListeButton.Click += wyczyscListe;
            daneLista.Controls.Add(wyczyscListeButton);

            // lista przedmiotow
            listaPrzedmiotow = new DataGridView();
            listaPrzedmiotow.Location = new Point(5, 220);
            listaPrzedmiotow.Size = new Size(325, 255);
            listaPrzedmiotow.ColumnCount = 3;
            listaPrzedmiotow.Columns[0].Name = "Nazwa";
            listaPrzedmiotow.Columns[0].ReadOnly = true;
            listaPrzedmiotow.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            listaPrzedmiotow.Columns[1].Name = "Wartość";
            listaPrzedmiotow.Columns[1].ReadOnly = true;
            listaPrzedmiotow.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            listaPrzedmiotow.Columns[2].Name = "Waga";
            listaPrzedmiotow.Columns[2].ReadOnly = true;
            listaPrzedmiotow.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            listaPrzedmiotow.AllowUserToAddRows = false;
            listaPrzedmiotow.CellMouseClick += wyswietlOpcjeListaPrzedmotow;
            listaPrzedmiotow.CellDoubleClick += dodajPrzedmiotDoPlecaka;
            daneLista.Controls.Add(listaPrzedmiotow);

            // rozmiar plecaka i wyniki
            plecak = new Panel();
            plecak.Location = new Point(350, 10);
            plecak.Size = new Size(335, 480);
            plecak.BackColor = Color.Blue;
            this.Controls.Add(plecak);

            // label rozmiar plecaka
            Label rozmiarLabel = new Label();
            rozmiarLabel.Location = new Point(5, 5);
            rozmiarLabel.Size = new Size(100, 40);
            rozmiarLabel.Text = "Rozmiar Plecka";
            rozmiarLabel.Font = new Font(wprowadzPrz.Font.Name, 10F, wprowadzPrz.Font.Style, wprowadzPrz.Font.Unit);
            plecak.Controls.Add(rozmiarLabel);

            // rozmiar plecaka
            rozmiarPlecaka = new NumericUpDown();
            rozmiarPlecaka.Location = new Point(115, 5);
            rozmiarPlecaka.Size = new Size(100, 40);
            rozmiarPlecaka.Maximum = 1000;
            plecak.Controls.Add(rozmiarPlecaka);

            // button pakuj optymalnie
            Button wstawOptymalnieButton = new Button();
            wstawOptymalnieButton.Location = new Point(5, 50);
            wstawOptymalnieButton.Size = new Size(130, 40);
            wstawOptymalnieButton.Text = "Zapakuj optymalnie";
            wstawOptymalnieButton.Click += pakujOptymalnie;
            plecak.Controls.Add(wstawOptymalnieButton);

            // lista zapakowanych przedmiotow
            listaZapakowanychPrzedmiotow = new DataGridView();
            listaZapakowanychPrzedmiotow.Location = new Point(5, 100);
            listaZapakowanychPrzedmiotow.Size = new Size(325, 255);
            listaZapakowanychPrzedmiotow.ColumnCount = 3;
            listaZapakowanychPrzedmiotow.Columns[0].Name = "Nazwa";
            listaZapakowanychPrzedmiotow.Columns[0].ReadOnly = true;
            listaZapakowanychPrzedmiotow.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            listaZapakowanychPrzedmiotow.Columns[1].Name = "Wartość";
            listaZapakowanychPrzedmiotow.Columns[1].ReadOnly = true;
            listaZapakowanychPrzedmiotow.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            listaZapakowanychPrzedmiotow.Columns[2].Name = "Waga";
            listaZapakowanychPrzedmiotow.Columns[2].ReadOnly = true;
            listaZapakowanychPrzedmiotow.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            listaZapakowanychPrzedmiotow.AllowUserToAddRows = false;
            listaZapakowanychPrzedmiotow.CellDoubleClick += wyjmijPrzedmiotZPlecaka;
            plecak.Controls.Add(listaZapakowanychPrzedmiotow);

            // Label laczna wartosc plecaka
            lacznaWarPlecTxt = new Label();
            lacznaWarPlecTxt.Location = new Point(5, 375);
            lacznaWarPlecTxt.Size = new Size(325, 40);
            lacznaWarPlecTxt.Text = "Łączna wartość plecaka: 0";
            lacznaWarPlecTxt.Font = new Font(lacznaWarPlecTxt.Font.Name, 14F, lacznaWarPlecTxt.Font.Style, lacznaWarPlecTxt.Font.Unit);
            plecak.Controls.Add(lacznaWarPlecTxt);

            // Label laczna waga plecaka
            lacznaWagPlecTxt = new Label();
            lacznaWagPlecTxt.Location = new Point(5, 425);
            lacznaWagPlecTxt.Size = new Size(325, 40);
            lacznaWagPlecTxt.Text = "Łączna waga plecaka: 0";
            lacznaWagPlecTxt.Font = new Font(lacznaWagPlecTxt.Font.Name, 14F, lacznaWagPlecTxt.Font.Style, lacznaWagPlecTxt.Font.Unit);
            plecak.Controls.Add(lacznaWagPlecTxt);

        }

        private void Form1_Load(object sender, EventArgs e) {
            
        }

        private void DodajPrzedmiot(object sender,EventArgs e) {
            if(nazwaPrzedmiotuTxt.Text != "" && wartoscPrzedmiotu.Value != 0 && wagaPrzedmiotu.Value != 0) {
                String[] dane = { nazwaPrzedmiotuTxt.Text, wartoscPrzedmiotu.Value.ToString(), wagaPrzedmiotu.Value.ToString() };
                przedmioty.Add(new Przedmiot() { nazwaPrz = nazwaPrzedmiotuTxt.Text, wartoscPrz = (int)wartoscPrzedmiotu.Value, wagaPrz = (int)wagaPrzedmiotu.Value, s = false, sOpt = false });
                listaPrzedmiotow.Rows.Add(dane);
                MessageBox.Show("Dodano przedmiot", "Komunikat");
                nazwaPrzedmiotuTxt.Clear();
                wartoscPrzedmiotu.Value = 0;
                wagaPrzedmiotu.Value = 0;
            }
            else {
                MessageBox.Show("Wartości nie mogą być puste lub zawierać 0!", "Komunikat");
            }
        }

        //private void usumPrzedmiot(object sender, EventArgs e) {
        private void dodajPrzedmiotDoPlecaka(object sender, DataGridViewCellEventArgs e) {
            int id = listaPrzedmiotow.CurrentCell.RowIndex;

            string nazwa = przedmioty[id].nazwaPrz;
            int wartosc = przedmioty[id].wartoscPrz;
            int waga = przedmioty[id].wagaPrz;

            if (zapakowanePrzedmioty.Count == 0) {
                dodajPrzedmiot(nazwa, wartosc, waga);
            }
            else {
                bool czyPrzedmiotJestWPlecaku = false;
                for(int i=0; i < zapakowanePrzedmioty.Count; i++) {
                    if(nazwa == zapakowanePrzedmioty[i].nazwaPrz && wartosc == zapakowanePrzedmioty[i].wartoscPrz && waga == zapakowanePrzedmioty[i].wagaPrz) {
                        czyPrzedmiotJestWPlecaku = false;
                    }
                    else {
                        czyPrzedmiotJestWPlecaku = true;
                    }
                }
                if (czyPrzedmiotJestWPlecaku) {
                    dodajPrzedmiot(nazwa, wartosc, waga);
                }
                else {
                    MessageBox.Show("Ten przedmiot jest już w plecaku!", "Komunikat");
                }
            }
        }

        private void dodajPrzedmiot(string nazwa, int wartosc, int waga) {
            zapakowanePrzedmioty.Add(new Przedmiot() {
                nazwaPrz = nazwa,
                wartoscPrz = wartosc,
                wagaPrz = waga
            });

            lacznaWartoscPlecaka += wartosc;
            lacznaWagaPlecaka += waga;

            lacznaWarPlecTxt.Text = String.Format("Łączna wartość plecaka: {0}", lacznaWartoscPlecaka);
            lacznaWagPlecTxt.Text = String.Format("Łączna waga plecaka: {0}", lacznaWagaPlecaka);

            listaZapakowanychPrzedmiotow.Rows.Clear();
            foreach (Przedmiot przedmiot in zapakowanePrzedmioty) {
                listaZapakowanychPrzedmiotow.Rows.Add(przedmiot.nazwaPrz, przedmiot.wartoscPrz, przedmiot.wagaPrz);
            }
        }

        private void wyswietlOpcjeListaPrzedmotow(object sender, DataGridViewCellMouseEventArgs e) {
            if(e.Button == MouseButtons.Right) {
                if(listaPrzedmiotow.Rows.Count > 0) {
                    listaPrzedmiotow.Rows[e.RowIndex].Selected = true;
                    listaPrzedmiotow.CurrentCell = listaPrzedmiotow.Rows[e.RowIndex].Cells[1];
                    ContextMenuStrip m = new ContextMenuStrip();
                    m.Items.Add("Usuń");
                    m.Items[0].Click += usunPrzedmiot;
                    m.Show(listaPrzedmiotow, e.Location);
                }
            }
        }

        private void usunPrzedmiot(object sender, EventArgs e) {
            int id = listaPrzedmiotow.CurrentCell.RowIndex;
            /*if (e.RowIndex == -1) {
                //MessageBox.Show("Blad", "Komunikat");
            }*/
            if (listaPrzedmiotow.Rows.Count == 0) {
                MessageBox.Show("Nie ma elemetów do usunięcia!", "Komunikat");
            }
            else {
                if (id == 1) {
                    //zapakowanePrzedmioty
                }
                listaPrzedmiotow.Rows.RemoveAt(id);
                przedmioty.RemoveAt(id);
                listaPrzedmiotow.Rows.Clear();
                foreach (Przedmiot przedmiot in przedmioty) {
                    listaPrzedmiotow.Rows.Add(przedmiot.nazwaPrz, przedmiot.wartoscPrz, przedmiot.wagaPrz);
                }
            }
        }

        private void wyczyscListe(object sender,EventArgs e) {
            przedmioty.Clear();//
            listaPrzedmiotow.Rows.Clear();
            zapakowanePrzedmioty.Clear();//
            listaZapakowanychPrzedmiotow.Rows.Clear();
            lacznaWartoscPlecaka = 0;//
            lacznaWagaPlecaka = 0;//
            rozmiarPlecaka.Value = 0;
            lacznaWarPlecTxt.Text = String.Format("Łączna wartość plecaka: {0}", lacznaWartoscPlecaka);
            lacznaWagPlecTxt.Text = String.Format("Łączna waga plecaka: {0}", lacznaWagaPlecaka);
        }

        private void pakujOptymalnie(object sender, EventArgs e) {
            if(przedmioty.Count == 0 && listaPrzedmiotow.RowCount == 0) {
                MessageBox.Show("Aby zapakować plecak, muszą być dostępne przedmioty!", "Komunikat");
            }
            else {
                lacznaWartoscPlecaka = 0;
                lacznaWagaPlecaka = 0;
                zapakowanePrzedmioty.Clear();
                listaZapakowanychPrzedmiotow.Rows.Clear();
                Plecak pl = new Plecak();
                pl.Dane((int)rozmiarPlecaka.Value, przedmioty);
                zapakowanePrzedmioty = pl.wynik();
                foreach (Przedmiot przedmiot in zapakowanePrzedmioty) {
                    listaZapakowanychPrzedmiotow.Rows.Add(przedmiot.nazwaPrz, przedmiot.wartoscPrz, przedmiot.wagaPrz);
                    lacznaWartoscPlecaka += przedmiot.wartoscPrz;
                    lacznaWagaPlecaka += przedmiot.wagaPrz;
                }
                lacznaWarPlecTxt.Text = String.Format("Łączna wartość plecaka: {0}", lacznaWartoscPlecaka);
                lacznaWagPlecTxt.Text = String.Format("Łączna waga plecaka: {0}", lacznaWagaPlecaka);
            }
        }

        private void wyjmijPrzedmiotZPlecaka(object sender, DataGridViewCellEventArgs e) {
            int id = listaZapakowanychPrzedmiotow.CurrentCell.RowIndex;
            if (listaZapakowanychPrzedmiotow.Rows.Count == 0) {
                MessageBox.Show("Nie ma elemetów do usunięcia!", "Komunikat");
            }
            else {

                int wartosc = zapakowanePrzedmioty[id].wartoscPrz;
                int waga = zapakowanePrzedmioty[id].wagaPrz;

                
                listaZapakowanychPrzedmiotow.Rows.RemoveAt(id);
                zapakowanePrzedmioty.RemoveAt(id);
                listaZapakowanychPrzedmiotow.Rows.Clear();
                foreach (Przedmiot przedmiot in zapakowanePrzedmioty) {
                    listaZapakowanychPrzedmiotow.Rows.Add(przedmiot.nazwaPrz, przedmiot.wartoscPrz, przedmiot.wagaPrz);
                }
                /*foreach(Przedmiot przedmiot in przedmioty) {
                    listaPrzedmiotow.Rows.Add(przedmiot.nazwaPrz, przedmiot.wartoscPrz, przedmiot.wagaPrz);
                }*/
                lacznaWartoscPlecaka -=wartosc;
                lacznaWagaPlecaka -= waga;
                lacznaWarPlecTxt.Text = String.Format("Łączna wartość plecaka: {0}", lacznaWartoscPlecaka);
                lacznaWagPlecTxt.Text = String.Format("Łączna waga plecaka: {0}", lacznaWagaPlecaka);
            }
        }

    }
}
