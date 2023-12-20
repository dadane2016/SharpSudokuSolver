using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using SudokuSolver;

namespace SharpSudokuSolver
{
    public partial class SudokuForm : Form
    {
        private Sudoku _Sudoku = new Sudoku();
        private Color _OriginalBackColor;

        public SudokuForm()
        {
            InitializeComponent();
        }

        private void ClearTableau()
        {
            Control _Cell;
            for (byte i = 0; i < 9; i++)
            {
                for (byte j = 0; j < 9; j++)
                {
                    _Cell = this.Controls.Find("L" + i.ToString() + "C" + j.ToString(), true).First();
                    (_Cell as Button).Text = "";
                    (_Cell as Button).BackColor = Color.LightGray;

                }
            }
        }

        private void SudokuForm_Shown(object sender, EventArgs e)
        {
            ClearTableau();
            _OriginalBackColor = L0C0.BackColor;
        }

        private void ClearGrid_Click(object sender, EventArgs e)
        {
            _Sudoku.Clear();
            ClearTableau();
        }

        private void DigitsMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Control c = DigitsMenu.SourceControl as Control;
            if (c is Button)
            {
                string _CtrlName = (c as Button).Name;
                int rowCell = int.Parse(_CtrlName.Substring(1, 1));
                int colCell = int.Parse(_CtrlName.Substring(3, 1));

                foreach (ToolStripItem Item in DigitsMenu.Items)
                {
                    if (Item.Name != "N0")
                    { 
                       Item.Enabled = _Sudoku.isSafe(rowCell, colCell, int.Parse(Item.Text));
                    }
                }
            }
        }

        private void DigitsMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Control c = DigitsMenu.SourceControl as Control;
            if (c is Button)
            {
                string _CtrlName = (c as Button).Name;
                int rowCell = int.Parse(_CtrlName.Substring(1, 1));
                int colCell = int.Parse(_CtrlName.Substring(3, 1));
                int valueCell = int.Parse(e.ClickedItem.Text);

                if (_Sudoku.isSafe(rowCell,colCell, valueCell) || (valueCell == 0))
                {
                    _Sudoku.GameMatrix[rowCell,colCell] = valueCell;

                    if (valueCell != 0)
                    {
                        (c as Button).Text = e.ClickedItem.Text;
                        (c as Button).BackColor = Color.ForestGreen;

                    }
                    else
                    {
                        (c as Button).Text = string.Empty;
                        (c as Button).BackColor = _OriginalBackColor;
                    }
                }
            }
        }

        private void AssignSudokuValuesToButtons()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Control C = this.Controls.Find("L" + i.ToString() + "C" + j.ToString(),true).First();
                    (C as Button).Text = _Sudoku.GameMatrix[i, j].ToString();
                }
            }
        }

        private void ResolveSudoku_Click(object sender, EventArgs e)
        {
            if (_Sudoku.Solve())
            {
                AssignSudokuValuesToButtons();
                MessageBox.Show("La grille est complétée!");
            }
            else
            {
                MessageBox.Show("Pas de solution pour cette grille!");
            }
        }
    }
}
