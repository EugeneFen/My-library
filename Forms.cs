using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_library
{
    public partial class Form1 : Form
    {
        private Hash hashtable = new Hash();
        private AVLTree root = new AVLTree();
        private List<Key_1> avltree { get; set; }
        public Form1()
        {
            hashtable.Read_File();
            root.Readfile(true);
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Writ_table_AVL()
        {
            Table_AVL.Rows.Clear();
            Table_AVL.Refresh();
            var avltree = new List<Key_1>();
            avltree = root.CopiAVL();
            Table_AVL.ColumnCount = 4;
            Table_AVL.Columns[0].HeaderCell.Value = "ФИО";
            Table_AVL.Columns[1].HeaderCell.Value = "Название";
            Table_AVL.Columns[2].HeaderCell.Value = "Жанр";
            Table_AVL.Columns[3].HeaderCell.Value = "Год";

            for (int i = 0; i < avltree.Count; i++)
            {
                var index = Table_AVL.Rows.Add();
                Table_AVL.Rows[index].Cells[0].Value = avltree[i].fio;
                Table_AVL.Rows[index].Cells[1].Value = avltree[i].name;
                Table_AVL.Rows[index].Cells[2].Value = avltree[i].genre;
                Table_AVL.Rows[index].Cells[3].Value = avltree[i].year;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var avltree = new List<Key_1>();
            avltree = root.CopiAVL();
            Table_AVL.ColumnCount = 4;
            Table_AVL.Columns[0].HeaderCell.Value = "ФИО";
            Table_AVL.Columns[1].HeaderCell.Value = "Название";
            Table_AVL.Columns[2].HeaderCell.Value = "Жанр";
            Table_AVL.Columns[3].HeaderCell.Value = "Год";

            for (int i = 0; i < avltree.Count; i++)
            {
                var index = Table_AVL.Rows.Add();
                Table_AVL.Rows[index].Cells[0].Value = avltree[i].fio;
                Table_AVL.Rows[index].Cells[1].Value = avltree[i].name;
                Table_AVL.Rows[index].Cells[2].Value = avltree[i].genre;
                Table_AVL.Rows[index].Cells[3].Value = avltree[i].year;
            }
        }

        private void AddAfter_Click(object sender, EventArgs e) //+++
        {
            string text1 = FIO.Text;
            string text2 = Country.Text;
            string text3 = Website.Text;

            if ((text1 != "") && (text2 != "") && (text3 != ""))
                if (hashtable.Search(text1) > 0)
                    MessageBox.Show("Такой автор уже есть", "Сообщение");
                else
                {
                    hashtable.Check(text1, text3, text2);
                    FIO.Text = "";
                    Country.Text = "";
                    Website.Text = "";
                    MessageBox.Show("Автор был добавлен", "Сообщение");
                }
            else
                MessageBox.Show("Поля оказались пусты", "Сообщение");
        }

        private void AddBook_Click(object sender, EventArgs e)
        {
            string text1 = FIO2.Text;
            string text2 = AddName.Text;
            string text3 = Genre1.Text;
            string text4 = Year.Text;

            if ((text1 != "") && (text2 != "") && (text3 != "") && (text4 != ""))
                if (hashtable.Search(text1) > 0)
                {
                    root.Check(text2, text1, text3, text4, true);
                    Writ_table_AVL();
                    AddName.Text = "";
                    Genre1.Text = "";
                    Year.Text = "";
                    MessageBox.Show("Автор был добавлен", "Сообщение");
                }                    
                else MessageBox.Show("Такого автора еще нет. Добавте для начала автора", "Сообщение");
            else MessageBox.Show("Поля оказались пусты", "Сообщение");
        }

        private void DeleteAfter_Click(object sender, EventArgs e)
        {
            string text1 = DelFIO.Text;

            if(text1 != "")
                if (hashtable.Search(text1) > 0)
                    if(root.Search_fio(text1)) //если у автора нет книг
                    {
                        hashtable.Del(text1);
                        DelFIO.Text = "";
                        MessageBox.Show("Автор удален", "Сообщение");
                    }                                      
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Чтобы удалить этого автора, надо удалить все его книги. Вы этого хотите?", "Сообщение", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            root.Delete_fio(text1, true); //удаляет все упоминания автора
                            hashtable.Del(text1);
                            MessageBox.Show("Автор удален", "Сообщение");
                            Writ_table_AVL();
                            DelFIO.Text = "";
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            MessageBox.Show("Автор небыл удален", "Сообщение");
                        }
                    }
                else MessageBox.Show("Такого автора нет", "Сообщение");
            else MessageBox.Show("Поля оказались пусты", "Сообщение");
            
        }

        private void DeleteBook_Click(object sender, EventArgs e)
        {
            string text1 = DelFIO2.Text;
            string text2 = DelName.Text;
            string text3 = DelGenre.Text;

            if ((text1 != "") && (text2 != ""))
                if(root.CheckRot()) //не пустое дерево
                    if(root.Poisk(text1, text3, text2)) //такая книга есть
                        {
                            root.Delete(text1, text3, text2, true); //удаляем
                            MessageBox.Show("Книга удалена", "Сообщение");
                            Writ_table_AVL();
                            DelName.Text = "";
                            DelGenre.Text = "";
                        }
                    else
                        MessageBox.Show("Такой книги нет, чтобы ее можно было удалить", "Сообщение");
                else
                MessageBox.Show("Нет ни одной книги", "Сообщение");
            else
                MessageBox.Show("Поля оказались пусты", "Сообщение");
        }

        private void button1_Click(object sender, EventArgs e) //хт отладка
        {
            Table_HT.Rows.Clear();
            Table_HT.Refresh();
            Notebook ht;
            avltree = root.CopiAVL();
            Table_HT.ColumnCount = 7;
            Table_HT.Columns[0].HeaderCell.Value = "№";
            Table_HT.Columns[1].HeaderCell.Value = "ХФ 1";
            Table_HT.Columns[2].HeaderCell.Value = "ХФ 2";
            Table_HT.Columns[3].HeaderCell.Value = "Статус";
            Table_HT.Columns[4].HeaderCell.Value = "ФИО";
            Table_HT.Columns[5].HeaderCell.Value = "Страна";
            Table_HT.Columns[6].HeaderCell.Value = "Сайт";

            for (int i = 0; i < 20; i++)
            {
                ht = hashtable.Copielement(i);
                var index = Table_HT.Rows.Add();
                Table_HT.Rows[index].Cells[0].Value = i;
                Table_HT.Rows[index].Cells[1].Value = ht.index1;
                Table_HT.Rows[index].Cells[2].Value = ht.index2;
                Table_HT.Rows[index].Cells[3].Value = ht.state;
                Table_HT.Rows[index].Cells[4].Value = ht.fio;
                Table_HT.Rows[index].Cells[5].Value = ht.country;
                Table_HT.Rows[index].Cells[6].Value = ht.website;
            }
        }

        private void Search1_Click_1(object sender, EventArgs e)
        {
            string text1 = FIO3.Text;
            string text2 = Genre2.Text;

            if ((text1 != "") && (text2 != ""))
                if (hashtable.Search(text1) > 0) //если такой автор впринципе сущ
                    if (root.CheckRot()) //если авл не пустое дерево
                        if (root.Rummage(text1, text2)) //если автор пишет в нужном жанре
                        {
                            int buff = hashtable.Search(text1); //индекс под которым стоит автор в хт
                            Notebook buff2 = hashtable.Copielement(buff);
                            Write_FIO.Text = buff2.fio;                            
                            Write_Countru.Text = buff2.country;
                            Write_WebSite.Text = buff2.website;
                            FIO3.Text = "";
                            Genre2.Text = "";
                        }
                        else
                            MessageBox.Show("Автор не пишет в таком жанре", "Сообщение");
                    else
                        MessageBox.Show("Список книг пуст", "Сообщение");
                else
                    MessageBox.Show("Такого автора нет", "Сообщение");
            else
                MessageBox.Show("Поля оказались пусты", "Сообщение");
        }

        private void WriteAVL_TextChanged(object sender, EventArgs e)
        {
        }

        private void AVL_Print_Click(object sender, EventArgs e)
        {
            WriteAVL.Clear();
            var avltree = new List<string>();
            avltree = root.Draw();

            WriteAVL.SelectedText = avltree[0];
            for (int i = 1; i < avltree.Count; i++)
            {
                WriteAVL.SelectedText = Environment.NewLine + avltree[i];
            }
                
            
        }
    }
}
