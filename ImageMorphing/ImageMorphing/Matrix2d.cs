using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageMorphing
{
    class Matrix2d
    {
        /*
        This class represents a typical Matrix2d.
        Implement dot product, det, inv methods.
        */

        public Matrix2d()
        {
            clear_matrix();
            error_threshold = 1e-6;
        }

        public Matrix2d(int rows, int cols)
        {
            set_shape(rows, cols);
            error_threshold = 1e-6;
        }

        public Matrix2d(List<List<double>> l)
        {
            set_matrix(l);
            error_threshold = 1e-6;
        }

        public Matrix2d(Matrix2d m)
        {
            set_matrix(m.m);
            error_threshold = 1e-6;
        }

        public List<List<double>> m;  // store values of the matrix in a List of List
        public int rows;
        public int cols;
        private double error_threshold;  // allow for small error, set as 1e-6

        // set the shape of the matrix and clear all elements
        public void set_shape(int rows, int cols)
        {
            clear_matrix();
            this.rows = rows;
            this.cols = cols;
            for (int i = 0; i < rows; i++)
            {
                m.Add(new List<double>(cols));
                for (int j = 0; j < cols; j++)
                {
                    m[i].Add(0.0);
                }
            }
        }
        // set the shape and values of the matrix using a 2d List
        public void set_matrix(List<List<double>> l)
        {
            set_shape(l.Count(), l[0].Count());
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    m[i][j] = l[i][j];
                }
            }
        }
        // clear matrix
        public void clear_matrix()
        {
            rows = 0;
            cols = 0;
            m = new List<List<double>>();
            m.Clear();
        }

        /*********************** some matrix calculation methods ***********************/
        // dot product between matrix
        public static Matrix2d dot_product(Matrix2d m1, Matrix2d m2)
        {
            int row1 = m1.rows, row2 = m2.rows, col1 = m1.cols, col2 = m2.cols;
            // assert (col1 == row2)
            double value = 0.0;
            Matrix2d result = new Matrix2d(row1, col2);
            for (int i = 0; i < row1; i++)
            {
                for (int j = 0; j < col2; j++)
                {
                    value = 0.0;
                    for (int k = 0; k < col1; k++)
                    {
                        value += m1.m[i][k] * m2.m[k][j];
                    }
                    result.m[i][j] = value;
                }
            }
            return result;
        }
        // inverse inv
        // using [A | I] method, transform A into an I and then I will become inv(A)
        public static Matrix2d inverse(Matrix2d m_in)
        {
            Matrix2d m = new Matrix2d(m_in);  // shouldn't change the value of m_in
            int row = m.rows;
            Matrix2d I = eye(row);
            double scale_ratio = 0.0;
            for (int i = 0; i < row; i++) // use the ith row to set the ith value in the remaining row = 0
            {
                if (Math.Abs(m.m[i][i]) < m.error_threshold) // swap a row with non-zero ith value to row i
                {
                    m.m[i][i] = 0.0;
                    for (int j = i + 1; j < row; j++)
                    {
                        if (Math.Abs(m.m[j][i]) >= m.error_threshold)
                        {
                            m.swap_row(i, j);
                            I.swap_row(i, j);
                            break;
                        }
                        else
                        {
                            m.m[j][i] = 0.0;
                        }
                    }
                    if (Math.Abs(m.m[i][i]) < m.error_threshold) return I;  // m is a uninverseable matrix
                }
                // subtract all other ith value of rows
                for (int j = 0; j < row; j++)
                {
                    if (i == j) continue;
                    if (Math.Abs(m.m[j][i]) < m.error_threshold)
                    {
                        m.m[j][i] = 0.0;
                        continue;
                    }
                    scale_ratio = m.m[j][i] / m.m[i][i];
                    m.subtract_row_ratio(j, i, scale_ratio);
                    I.subtract_row_ratio(j, i, scale_ratio);
                }
            }
            // set every A[i, i] values as 1
            for (int i = 0; i < row; i++)
            {
                scale_ratio = m.m[i][i];
                m.divide_row(i, scale_ratio);
                I.divide_row(i, scale_ratio);
            }
            return I;
        }
        // row1 -= (row2 * ratio)
        public void subtract_row_ratio(int row1, int row2, double ratio)
        {
            for (int i = 0; i < cols; i++)
            {
                m[row1][i] -= (m[row2][i] * ratio);
            }
        }
        // swap two rows of the matrix
        public void swap_row(int row1, int row2)
        {
            double temp = 0.0;
            for (int i = 0; i < cols; i++)
            {
                temp = m[row1][i];
                m[row1][i] = m[row2][i];
                m[row2][i] = temp;
            }
        }
        // swap two columns of the matrix
        public void swap_col(int col1, int col2)
        {
            double temp = 0.0;
            for (int i = 0; i < rows; i++)
            {
                temp = m[i][col1];
                m[i][col1] = m[i][col2];
                m[i][col2] = temp;
            }
        }
        // multiply a row by a certain value
        public void multiply_row(int row, double value)
        {
            for (int i = 0; i < cols; i++)
            {
                m[row][i] *= value;
            }
        }
        // divide a row by a certain value
        public void divide_row(int row, double value)
        {
            multiply_row(row, 1.0 / value);
        }
        // multiply a column by a certain value
        public void multiply_col(int col, double value)
        {
            for (int i = 0; i < rows; i++)
            {
                m[i][col] *= value;
            }
        }
        // divide a column by a certain value
        public void divide_col(int col, double value)
        {
            multiply_col(col, 1.0 / value);
        }
        // add one row to another row
        public void add_row(int row1, int row2)
        {
            // row1 += row2
            for (int i = 0; i < cols; i++)
            {
                m[row1][i] += m[row2][i];
            }
        }
        // subtract one row to another row
        public void subtract_row(int row1, int row2)
        {
            // row1 -= row2
            for (int i = 0; i < cols; i++)
            {
                m[row1][i] -= m[row2][i];
            }
        }
        // add one column to another column
        public void add_col(int col1, int col2)
        {
            // col1 += col2
            for (int i = 0; i < rows; i++)
            {
                m[i][col1] += m[i][col2];
            }
        }
        // subtract one column to another column
        public void subtract_col(int col1, int col2)
        {
            // col1 -= col2
            for (int i = 0; i < rows; i++)
            {
                m[i][col1] -= m[i][col2];
            }
        }

        /*********************** some common types of matrix ***********************/
        // eye matrix
        public static Matrix2d eye(int dim)
        {
            Matrix2d new_m = new Matrix2d(dim, dim);
            for (int i = 0; i < dim; i++)
            {
                new_m.m[i][i] = 1.0;
            }
            return new_m;
        }

        /*********************** override some operators ***********************/
        // operator * between matrix and a double value
        public static Matrix2d operator *(Matrix2d m, double value)
        {
            Matrix2d new_m = new Matrix2d(m.rows, m.cols);
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    new_m.m[i][j] = m.m[i][j] * value;
                }
            }
            return new_m;
        }
        // operator / between matrix and a double value
        public static Matrix2d operator /(Matrix2d m, double value)
        {
            return m * (1.0 / value);
        }
    }
}
