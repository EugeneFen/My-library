using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace My_library
{
	public struct Notebook
	{
		public string website, fio, country;
		public int state, index1, index2;
	};

	class Hash
	{
		private Notebook[] table; //таблица
		private int buffer_size; //размер самого массива


		private int Hash_One_Function(string key) //первичная хеш-функция  ++++
		{
			int size_key = key.Length;
			int summ = 0;
			int buff;
			for (int i = 0; i < size_key; i++)
			{
				buff = Convert.ToInt32(key[i]) + 128; //зачение буквы !!!!!!!!!!!!!!!!!!!!!!
				summ += buff;
			}
			return (summ % buffer_size);
		}

		private int Hash_Two_Function(int index, int j) //++++
		{
			return ((index + j * 1) % buffer_size);
		}

		private int Collision(int index, string buff) //++++
		{
			int step = 0;
			int i = 0;
			int j = 1;
			int two_index = index;
			while (i < buffer_size)
			{
				two_index = Hash_Two_Function(index, j);

				if (table[two_index].fio == buff) return -1;
				else if (table[two_index].state == 0) return two_index;
				else if (table[two_index].state == 2)
				{
					if (Search(buff, ref step) < 0)
					{
						return two_index;
					}
					else return -1;
				}
				j++;
				i++;
			}
			return -1;
		}

		private int Decision(int index, string value) //-----
		{
			int two_index = index;
			int i = 0;
			int j = 1;
			while (i < buffer_size)
			{
				two_index = Hash_Two_Function(index, j);
				if (table[two_index].fio == value && table[two_index].state == 1) return two_index;
				i++;
				j++;

				Console.Write(two_index);
				Console.WriteLine(" index del");
			}
			return -1;
		}

		private bool Check_Letter(string value) //проверяет буквы на коректность
		{
			int i, j;
			j = 0;
			i = value.Length;

			do
			{
				if (((value[j] >= 'а') && (value[j] <= 'я')) || ((value[j] >= 'А') && (value[j] <= 'Я')) || (value[j] == ' ') || ((value[j] >= 'A') && (value[j] <= 'Z')) || ((value[j] >= 'a') && (value[j] <= 'z')))
					j++;
				else
					return false;
			} while (j != i);
			return true;
		}

		public int Size_table()
		{
			return buffer_size;
		}

		public Notebook Copielement(int index)
		{
			return table[index];
		}

		public Hash() //конструктор ++++
		{
			Console.WriteLine("Start");
			buffer_size = 20; //размер таблицы
			table = new Notebook[buffer_size]; //сама таблица

			for (int i = 0; i < buffer_size; i++)
			{
				table[i].country = table[i].fio = table[i].website = "_"; //заполняем таблицу пробелами
				table[i].state = 0; table[i].index1 = -1; table[i].index2 = -1;

            }
		}

		public bool Add(string fio_v, string website_v, string country_v) //добавление  !!!!!!!!!!!!!!!  ++++
		{
			int index = Hash_One_Function(fio_v); //вычисляем место для эл
			int step = 0;
			
			if (table[index].fio == fio_v && table[index].state == 1) return false;
			else
			if (table[index].state == 1)
			{

				int two_index = Collision(index, fio_v);
				if (two_index >= 0)
				{
					table[two_index].website = website_v;
					table[two_index].fio = fio_v;
					table[two_index].country = country_v;
					table[two_index].state = 1;
					table[two_index].index1 = index;
                                        table[two_index].index2 = two_index;
                                        return true;
				}
				else return false;
			}
			else
			if (table[index].state == 0)
			{
				table[index].website = website_v;
				table[index].fio = fio_v;
				table[index].country = country_v;
				table[index].state = 1;
				table[index].index1 = index;
                                table[index].index2 = -1;
                                return true;
			}
			else
			if (table[index].state == 2)
			{
				if (Search(fio_v, ref step) < 0)
				{
					table[index].website = website_v;
					table[index].fio = fio_v;
					table[index].country = country_v;
					table[index].state = 1;
                                        table[index].index1 = index;
                                        table[index].index2 = -1;
                                        return true;
				}
				else
				{
					return false;
				}
			}
			return false;
		}

		public void Check(string fio_v, string website_v, string country_v)
		{
			if (Check_Letter(fio_v) && Check_Letter(country_v))
				Add(fio_v, website_v, country_v);
		}

		public bool Del(string fio_v) //удаление  ++++
		{
			int index = Hash_One_Function(fio_v);

			if (table[index].fio == fio_v && table[index].state == 1)
			{
				table[index].state = 2;
				return true;
			}
			else
			{
				int two_index = Decision(index, fio_v);
				if (two_index >= 0)
				{
					table[two_index].state = 2;
					return true;
				}
				else return false;
			}
		}

		public int Search(string fio_v, ref int score) //поиск ++++
		{
			int index = Hash_One_Function(fio_v);
			int two_index = index;
			int i = 0;
			int j = 1;
			score = 0;
			while (table[two_index].fio != "_" && i < buffer_size)
			{
				score++;
				if (table[two_index].fio == fio_v && table[two_index].state == 1) return two_index;
				two_index = Hash_Two_Function(index, j);
				j++;
				i++;
			}
			return -1;
		}

		public void Read_File()
		{
			string curfile = @"Author.txt";

			if (File.Exists(curfile))
			{
				StreamReader file = new StreamReader("Author.txt");

				string fio_v, website_v, country_v;
				fio_v = website_v = country_v = "";

				while (!file.EndOfStream)
				{
					buff = file.ReadLine();
					string litter = "*";
					int j = 0;

					for (int i = 0; i < 2; i++)
					{
						while (buff[j] != litter[0]) j++;
						if (i == 0) website_v = buff.Substring(0, j);
						if (i == 1) fio_v = buff.Substring(0, j);

						buff = buff.Remove(0, j + 1);
						j = 0;
					}
					int size_buff = buff.Length;
					country_v = buff.Substring(0, size_buff);

					Add(fio_v, website_v, country_v);
				}
				file.Close();
			}
		}

		~Hash() //деструктор
		{

		}
		
		public void Write_file()
                {
			string curfile = @"Author.txt";

			if (File.Exists(curfile))
			{
				File.WriteAllText(curfile, ""); //очистка файла
				StreamWriter file = new StreamWriter("Author.txt");


				for (int i = 0; i < buffer_size; i++)
				{
					if(table[i].fio != "_") file.WriteLine(table[i].website + "*" + table[i].fio + "*" + table[i].country);
				}
				file.Close();
			}
		}
		
		public void Write_History(int index)
                {
			string curfile = @"History.txt";

			if (File.Exists(curfile))
			{
				File.WriteAllText(curfile, ""); //очистка файла
				StreamWriter file = new StreamWriter("History.txt");
				file.WriteLine(table[index].website + "  " + table[index].fio + "  " + table[index].country);
				file.Close();
			}
		}
	};
}
