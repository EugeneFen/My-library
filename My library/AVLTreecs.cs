using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace My_library
{
		public class Key_1 //класс ключа
		{
			public Key_1 next;
			public string fio, name, genre, year; //fio - фио автора, name - название книги, genre - жанр книги, year - год написания

			public Key_1(string name_v, string fio_v, string genre_v, string year_v)
			{
				next = null;
				fio = fio_v;
				name = name_v;
				genre = genre_v;
				year = year_v;
			}
		};

		class AVLTree
		{
			class Node //класс дерева
			{
				public int balans;
				public Key_1 key;
				public Node left;
				public Node right;

				public Node(string name_v, string fio_v, string genre_v, string year_v)
				{
					left = null;
					right = null;
					balans = 0;
					key = new Key_1(name_v, fio_v, genre_v, year_v);
				}
			};

			Node root; //корень

			private void Add(ref Node tree, string name_v, string fio_v, string genre_v, string year_v, ref bool flag) //добавление
			{
				Key_1 vertex;
				Node tree1;
				Node tree2;

				if (tree == null)
					tree = new Node(name_v, fio_v, genre_v, year_v);
				else if ((string.Compare(tree.key.fio, fio_v) > 0) || ((tree.key.fio == fio_v) && (string.Compare(tree.key.genre, genre_v) > 0)))
				{
					Add(ref tree.left, name_v, fio_v, genre_v, year_v, ref flag);

					if (flag)
						switch (tree.balans)
						{
							case 1: tree.balans = 0; flag = false; break;
							case 0: tree.balans = -1; break;
							case -1:
								tree1 = tree.left;
								if (tree1.balans == -1)
								{
									tree.left = tree1.right;
									tree1.right = tree;
									tree.balans = 0;
									tree = tree1;
								}
								else
								{
									tree2 = tree1.right;
									tree1.right = tree2.left;
									tree2.left = tree1;
									tree.left = tree2.right;
									tree2.right = tree;
									if ((tree.left != null && tree.right != null) || (tree.left == null && tree.right == null))
										tree.balans = 0;
									else
										if (tree.left != null)
										tree.balans = -1;
									else
										if (tree.right != null)
										tree.balans = 1;

									if ((tree1.left != null && tree1.right != null) || (tree1.left == null && tree1.right == null))
										tree1.balans = 0;
									else
										if (tree1.left != null)
										tree1.balans = -1;
									else
										if (tree1.right != null)
										tree1.balans = 1;
									tree = tree2;
								}
								tree.balans = 0;
								flag = false;
								break;
						}
				}
				else if ((string.Compare(tree.key.fio, fio_v) < 0) || ((tree.key.fio == fio_v) && (string.Compare(tree.key.genre, genre_v) < 0)))
				{
					Add(ref tree.right, name_v, fio_v, genre_v, year_v, ref flag);

					if (flag)
						switch (tree.balans)
						{
							case -1: tree.balans = 0; flag = false; break;
							case 0: tree.balans = 1; break;
							case 1:
								tree1 = tree.right;
								if (tree1.balans == 1)
								{
									tree.right = tree1.left;
									tree1.left = tree;
									tree.balans = 0;
									tree = tree1;
								}
								else
								{
									tree2 = tree1.left;
									tree1.left = tree2.right;
									tree2.right = tree1;
									tree.right = tree2.left;
									tree2.left = tree;
									if ((tree.left != null && tree.right != null) || (tree.left == null && tree.right == null))
										tree.balans = 0;
									else
										if (tree.left != null)
										tree.balans = -1;
									else
										if (tree.right != null)
										tree.balans = 1;

									if ((tree1.left != null && tree1.right != null) || (tree1.left == null && tree1.right == null))
										tree1.balans = 0;
									else
										if (tree1.left != null)
										tree1.balans = -1;
									else
										if (tree1.right != null)
										tree1.balans = 1;

									tree = tree2;
								}
								tree.balans = 0;
								flag = false;
								break;
						}
				}
				else
					if (tree.key.next == null)
				{
					vertex = new Key_1(name_v, fio_v, genre_v, year_v);
					tree.key.next = vertex;
					flag = false;
				}
				else
				{
					vertex = new Key_1(name_v, fio_v, genre_v, year_v);
					Key_1 buff;
					buff = tree.key;
					while (buff.next != null) buff = buff.next;
					buff.next = vertex;
					flag = false;
				}

			}

			private void BalanceL(ref Node tree, ref bool flag) //баланс при удалении
			{
				Node tree1;
				Node tree2;

				switch (tree.balans)
				{
					case -1: tree.balans = 0; break;
					case 0: tree.balans = 1; flag = false; break;
					case 1:
						tree1 = tree.right;
						if (tree1.balans >= 0)
						{
							tree.right = tree1.left;
							tree1.left = tree;
							if (tree1.balans == 0)
							{
								tree.balans = 1;
								tree1.balans = -1;
								flag = false;
							}
							else
							{
								tree.balans = 0;
								tree1.balans = 0;
							}
							tree = tree1;
						}
						else
						{
							tree2 = tree1.left;
							tree1.left = tree2.right;
							tree2.right = tree1;
							tree.right = tree2.left;
							tree2.left = tree;
							if (tree2.balans == 1)
								tree.balans = -1;
							else
								tree.balans = 0;

							if (tree2.balans == -1)
								tree1.balans = 1;
							else
								tree1.balans = 0;

							tree = tree2;
							tree2.balans = 0;
						}
						break;
				}
			}

			private void BalanceR(ref Node tree, ref bool flag) //баланс при удалении
			{
				Node tree1;
				Node tree2;

				switch (tree.balans)
				{
					case 1: tree.balans = 0; break;
					case 0: tree.balans = -1; flag = false; break;
					case -1:
						tree1 = tree.left;
						if (tree1.balans <= 0)
						{
							tree.left = tree1.right;
							tree1.right = tree;
							if (tree1.balans == 0)
							{
								tree.balans = -1;
								tree1.balans = 1;
								flag = false;
							}
							else
							{
								tree.balans = 0;
								tree1.balans = 0;
							}
							tree = tree1;
						}
						else
						{
							tree2 = tree1.right;
							tree1.right = tree2.left;
							tree2.left = tree1;
							tree.left = tree2.right;
							tree2.right = tree;
							if (tree2.balans == -1)
								tree.balans = 1;
							else
								tree.balans = 0;

							if (tree2.balans == 1)
								tree1.balans = -1;
							else
								tree1.balans = 0;

							tree = tree2;
							tree2.balans = 0;
						}
						break;
				}
			}

			private void DeleteTwo(ref Node r, ref bool flag, ref Node q) //
			{
				if (r.left != null)
				{
					DeleteTwo(ref r.left, ref flag, ref q);
					if (flag) BalanceR(ref r, ref flag);
				}
				else
				{
					q.key.fio = r.key.fio;
					q.key.name = r.key.name;
					q.key.year = r.key.year;
					q.key.genre = r.key.genre;
					q.key.next = r.key.next;
					q = r;
					r = r.right;
					flag = true;
				}
			}

			private void DeleteOne(ref Node tree, ref bool flag, string fio_v, string name_v) //удаляет книгу
			{
				Node q;

				if (tree != null)
					if ((tree.key.fio == fio_v) && (tree.key.next != null)) //
					{
						Key_1 pointer = tree.key;

						if (pointer.fio == fio_v && pointer.name == name_v)//
						{
							tree.key = tree.key.next;
							pointer.next = null;
							flag = false;
						}
						else
						{
							do
							{
								pointer = pointer.next;
								if (pointer.fio == fio_v && pointer.name == name_v) //
								{
									Key_1 pointer1 = tree.key;
									while (pointer1.next != pointer) pointer1 = pointer1.next; //
									pointer1.next = pointer.next;
									pointer.next = null;
									pointer = pointer1;
									flag = false;
								}
							} while (pointer.next != null);
							if (pointer.next == null && pointer.fio == fio_v && pointer.name == name_v) //
							{
								Key_1 pointer1 = tree.key;
								while (pointer1.next != pointer) pointer1 = pointer1.next; //
								pointer1.next = null;
								pointer = pointer1;
								flag = false;
							}
						}
					}
					else if ((string.Compare(tree.key.fio, fio_v) > 0) || ((tree.key.fio == fio_v) && (string.Compare(tree.key.name, name_v) > 0)))
					{
						DeleteOne(ref tree.left, ref flag, fio_v, name_v);
						if (flag) BalanceL(ref tree, ref flag);
					}
					else if ((string.Compare(tree.key.fio, fio_v) < 0) || ((tree.key.fio == fio_v) && (string.Compare(tree.key.name, name_v) < 0)))
					{
						DeleteOne(ref tree.right, ref flag, fio_v, name_v);
						if (flag) BalanceR(ref tree, ref flag);
					}
					else
					{
						q = tree;
						if (q.right == null)
						{
							tree = q.left;
							flag = true;
						}
						else if (q.left == null)
						{
							tree = q.right;
							flag = true;
						}
						else
						{
							DeleteTwo(ref q.right, ref flag, ref q);
							if (flag) BalanceL(ref tree, ref flag); //!!!!!!!!!!
						}
					}
			}

			private void DeleteOne_fio(ref Node tree, ref bool flag, string fio_v) //удаляет книгу
			{
				Node q;

				if (tree != null)
					if (string.Compare(tree.key.fio, fio_v) > 0)
					{
						DeleteOne_fio(ref tree.left, ref flag, fio_v);
						if (flag) BalanceL(ref tree, ref flag);
					}
					else if (string.Compare(tree.key.fio, fio_v) < 0)
					{
						DeleteOne_fio(ref tree.right, ref flag, fio_v);
						if (flag) BalanceR(ref tree, ref flag);
					}
					else
					{
						q = tree;
						if (q.right == null)
						{
							tree = q.left;
							flag = true;
						}
						else if (q.left == null)
						{
							tree = q.right;
							flag = true;
						}
						else
						{
							DeleteTwo(ref q.right, ref flag, ref q);
							if (flag) BalanceL(ref tree, ref flag); //!!!!!!!!!!
						}
					}
			}

			private bool Check_Number(string value) //проверяет год на коректность
			{
				int i, j;
				j = 0;
				i = value.Length;

				do
				{
					if ((value[j] >= '0') && (value[j] <= '9'))
						j++;
					else
						return false;
				} while (j != i);
				return true;
			}

			private bool Check_Letter(string value) //проверяет буквы на коректность
			{
				int i, j;
				j = 0;
				i = value.Length;

				do
				{
					//Console.Write(value);
					if (((value[j] >= 'а') && (value[j] <= 'я')) || ((value[j] >= 'А') && (value[j] <= 'Я')) || (value[j] == ' ') || ((value[j] >= 'A') && (value[j] <= 'Z')) || ((value[j] >= 'a') && (value[j] <= 'z')))
						j++;
					else
						return false;
				} while (j != i);
				return true;
			}

			private void Cleaning(ref Node tree) //очистка
			{
				if (tree != null)
				{
					Cleaning(ref tree.left);
					Cleaning(ref tree.right);
					tree = null;
				}
			}

			private void Drawing(ref Node tree, int h) //выписывание
			{
				if (tree != null)
				{
					h += 4;
					Drawing(ref tree.right, h);
					if (tree.key.next == null)
					{
						for (int i = 0; i < h; i++) Console.Write("  ");

						Console.Write(tree.key.fio); Console.Write("  ");
						Console.Write(tree.key.name); Console.Write("  ");
						Console.WriteLine(tree.balans);
					}
					else
					{
						for (int i = 0; i < h; i++) Console.Write("  ");

						Console.Write(tree.key.fio); Console.Write("  ");
						Console.Write(tree.key.name); Console.Write("  ");
						Console.Write(tree.balans); Console.Write("   ");

						Key_1 buff = tree.key;
						do
						{
							buff = buff.next;

							Console.Write(buff.fio); Console.Write("  ");
							Console.Write(buff.name); Console.Write("   ");

						} while (buff.next != null);
						Console.WriteLine();
					}
					Drawing(ref tree.left, h);
				}
			}

			public bool Poisk(string fio_v, string genre_v, string name_v) //поиск книги
			{
				Node curr = root;
				while (curr != null)
				{
					if (fio_v == curr.key.fio && genre_v == curr.key.genre && name_v == curr.key.name) return true;
					if (fio_v == curr.key.fio && genre_v == curr.key.genre && curr.key.next != null)
					{
						Key_1 buff = curr.key;
						do
						{
							buff = buff.next;
							if (name_v == buff.name) return true;
						} while (buff.next != null);
					}
					if ((string.Compare(curr.key.fio, fio_v) > 0) || (fio_v == curr.key.fio && (string.Compare(curr.key.genre, genre_v) > 0))) curr = curr.left;
					else curr = curr.right;
				}
				return false;
			}

			public bool Rummage(string fio_v, string genre_v) //поиск автора с жанром
			{
				Node curr = root;
				while (curr != null)
				{
					if (fio_v == curr.key.fio && genre_v == curr.key.genre) return true;					
					if ((string.Compare(curr.key.fio, fio_v) > 0) || (fio_v == curr.key.fio && (string.Compare(curr.key.genre, genre_v) > 0))) curr = curr.left;
					else curr = curr.right;
				}
				return false;
			}

			public bool Search_fio(string fio_v) //поиск фамилии
			{
				Node curr = root;
				while (curr != null)
				{
					if (fio_v == curr.key.fio) return false;                
					if (string.Compare(curr.key.fio, fio_v) > 0) curr = curr.left;
					else curr = curr.right;
				}
				return true;
			}

			private void Lrb(Node tree, ref List<Key_1> list) //
			{
				if (tree != null)
				{
					Lrb(tree.left, ref list);
					Lrb(tree.right, ref list);

					if (tree.key.next == null)
					{
						list.Add(tree.key);
					}
					else
					{
						Key_1 buff = tree.key;
						do
						{
							list.Add(buff);

							buff = buff.next;
						} while (buff.next != null);
					}
				}
			}

			public bool Check(string name_v, string fio_v, string genre_v, string year_v, bool flag) //добавление
			{
				if (Check_Number(year_v) && Check_Letter(name_v) && Check_Letter(fio_v) && Check_Letter(genre_v))
				{
					if (root == null)
						root = new Node(name_v, fio_v, genre_v, year_v);
					else
						Add(ref root, name_v, fio_v, genre_v, year_v, ref flag);
					return true;
				}
				else
				{
					return false;
				}
			}

			~AVLTree() //
			{
				Cleaning(ref root);
			}

			public void Delete(string fio_v, string name_v, bool flag) //удаляет книгу
			{
					DeleteOne(ref root, ref flag, fio_v, name_v);
			}

			public void Delete_fio(string fio_v, bool flag) //удаляет книгу
			{
				while(!Search_fio(fio_v))
					DeleteOne_fio(ref root, ref flag, fio_v);
			}

			public bool CheckRot()
			{			
			if (root != null)
				return true;
			else return false;
        }
		
			public void Readfile(bool flag) //читает данные из файла
			{
				StreamReader file = new StreamReader("Book.txt");
				string fio_v, name_v, genre_v, year_v;

				while (!file.EndOfStream)
				{
					name_v = file.ReadLine();
					fio_v = file.ReadLine();
					year_v = file.ReadLine();
					genre_v = file.ReadLine();

					Check(name_v, fio_v, genre_v, year_v, flag);
				}
				file.Close();
			}

			public AVLTree() //конструктор
			{
			}

			public void Clear() //очищает дерево
			{
				Cleaning(ref root);
			}

			public void Draw(int h) //выписывает дерево
			{
				if (root == null)
					Console.WriteLine("Not element");
				else
					Drawing(ref root, h);
			}

			public List<Key_1> CopiAVL()
			{
				var avltree = new List<Key_1>();
				Lrb(root, ref avltree);
				return avltree;
			}
		};
	}
