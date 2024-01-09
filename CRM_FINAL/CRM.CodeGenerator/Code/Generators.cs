using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CRM.CodeGenerator
{
    internal static class CodeGenerators
    {
        internal static string GenerateListXamlCode(Data.Table table)
        {
            using (StreamReader reader = new StreamReader("Templates/List.xaml.Template"))
            {
                string template = reader.ReadToEnd();
                template = template.Replace("[TableName]", table.Name);

                #region DataGridColumns
                string dataGridColumnsItems = string.Empty;
                foreach (Data.Column column in table.Columns)
                {
                    if (column.ReferencedTable != null)
                    {
                        dataGridColumnsItems += @"<DataGridComboBoxColumn Header=""" + RemoveIDFromName(column.Description) + @""" x:Name=""" + RemoveIDFromName(column.ReferencedTable.Name) + @"Column"" SelectedValueBinding=""{Binding Path=" + column.Name + @", UpdateSourceTrigger=PropertyChanged}"" SelectedValuePath=""ID"" DisplayMemberPath=""Name"" />" + System.Environment.NewLine;
                    }
                    else if (column.Type == "tinyint" && column.Description.Contains("نوع"))
                    {
                        dataGridColumnsItems += @"<DataGridComboBoxColumn Header=""" + RemoveEnumFromName(column.Description) + @""" x:Name=""" + RemoveIDFromName(column.Name) + @"Column"" SelectedValueBinding=""{Binding Path=" + column.Name + @", UpdateSourceTrigger=PropertyChanged}"" SelectedValuePath=""ID"" DisplayMemberPath=""Name"" />" + System.Environment.NewLine;
                    }
                    else if (column.Type == "bit")
                    {
                        dataGridColumnsItems += @"<DataGridCheckBoxColumn Header=""" + column.Description + @""" Binding=""{Binding Path=" + column.Name + @"}""/>" + System.Environment.NewLine;
                    }
                    else
                    {
                        dataGridColumnsItems += @"<DataGridTextColumn Header=""" + column.Description + @"""  Binding=""{Binding Path=" + column.Name + @", UpdateSourceTrigger=PropertyChanged}"" />" + System.Environment.NewLine;
                    }
                }

                template = template.Replace("[DataGridColumns]", dataGridColumnsItems);
                #endregion

                #region Search
                string gridRows = string.Empty;
                for (int i = 0; i <= table.Columns.Count; i = i + 3)
                {
                    gridRows += @"<RowDefinition Height=""25"" />" + Environment.NewLine;
                }
                string searchGrid =
@"
<Grid.RowDefinitions>
" + gridRows + @"<RowDefinition Height=""Auto"" />
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width=""Auto"" />
        <ColumnDefinition Width=""*"" />
        <ColumnDefinition Width=""10"" />
        <ColumnDefinition Width=""Auto"" />
        <ColumnDefinition Width=""*"" />
        <ColumnDefinition Width=""10"" />
        <ColumnDefinition Width=""Auto"" />
        <ColumnDefinition Width=""*"" />
    </Grid.ColumnDefinitions>
";

                string searchGridControls = string.Empty;

                for (int i = 0, col = 0, row = 0; i < table.Columns.Count; i++, col++, row = i / 3)
                {
                    if (col % 8 == 2 || col % 8 == 5) col++;

                    Data.Column column = table.Columns[i];

                    if (column.Name.ToLower() == "id") continue;

                    if (column.ReferencedTable != null || (column.Type == "tinyint" && column.Description.Contains("نوع")))
                    {
                        searchGridControls += @"<TextBlock                      Grid.Row=""" + row + @""" Grid.Column=""" + (col % 8) + @""" Text=""" + RemoveEnumFromName(RemoveIDFromName(column.Description)) + @":"" />" + Environment.NewLine;
                        searchGridControls += @"<UserControls:CheckableComboBox Grid.Row=""" + row + @""" Grid.Column=""" + (++col % 8) + @""" x:Name=""" + RemoveEnumFromName(RemoveIDFromName(column.Name)) + @"ComboBox""/>" + Environment.NewLine + Environment.NewLine;
                    }
                    else if (column.Type == "datetime" || column.Type == "smalldatetime")
                    {
                        searchGridControls += @"<TextBlock      Grid.Row=""" + row + @""" Grid.Column=""" + (col % 8) + @""" Text=""" + column.Description + @":"" />" + Environment.NewLine;
                        searchGridControls += @"<cal:DatePicker Grid.Row=""" + row + @""" Grid.Column=""" + (++col % 8) + @""" x:Name=""" + column.Name + @"Date""/>" + Environment.NewLine + Environment.NewLine;
                    }
                    else if (column.Type == "bit")
                    {
                        searchGridControls += @"<TextBlock  Grid.Row=""" + row + @""" Grid.Column=""" + (col % 8) + @""" Text=""" + column.Description + @":"" />" + Environment.NewLine;
                        searchGridControls += @"<CheckBox   Grid.Row=""" + row + @""" Grid.Column=""" + (++col % 8) + @""" x:Name=""" + column.Name + @"CheckBox""/>" + Environment.NewLine + Environment.NewLine;
                    }
                    else
                    {
                        searchGridControls += @"<TextBlock  Grid.Row=""" + row + @""" Grid.Column=""" + (col % 8) + @""" Text=""" + column.Description + @":"" />" + Environment.NewLine;
                        searchGridControls += @"<TextBox    Grid.Row=""" + row + @""" Grid.Column=""" + (++col % 8) + @""" x:Name=""" + column.Name + @"TextBox""/>" + Environment.NewLine + Environment.NewLine;
                    }
                }

                template = template.Replace("[SearchGridControls]", searchGrid + searchGridControls);
                #endregion

                return template;
            }
        }

        internal static string GenerateListCsCode(Data.Table table)
        {
            using (StreamReader reader = new StreamReader("Templates/List.cs.Template"))
            {
                string template = reader.ReadToEnd();
                template = template.Replace("[TableName]", table.Name);

                #region InitializeMethod
                string initializeMethodItems = string.Empty;
                foreach (Data.Column column in table.Columns)
                {
                    if (column.ReferencedTable != null)
                    {
                        initializeMethodItems += string.Format("{0}Column.ItemsSource = Data.{0}DB.Get{0}Checkable();{1}", column.ReferencedTable.Name, System.Environment.NewLine);
                    }
                    else if (column.Type == "tinyint" && column.Description.Contains("نوع"))
                    {
                        initializeMethodItems += string.Format("{0}Column.ItemsSource = Helper.GetEnumNameValue(typeof(DB.{0}));{1}", RemoveIDFromName(column.Name), System.Environment.NewLine);
                    }
                }

                template = template.Replace("[InitializeMethod]", initializeMethodItems);
                #endregion

                #region ResetSearchFormMethod
                string resetSearchFormMethod = string.Empty;
                foreach (Data.Column column in table.Columns)
                {
                    if (column.ReferencedTable != null || (column.Type == "tinyint" && column.Description.Contains("نوع")))
                    {
                        resetSearchFormMethod += string.Format("{0}ComboBox.Reset();{1}", RemoveEnumFromName(RemoveIDFromName(column.Name)), Environment.NewLine);
                    }
                    else if (column.Type == "datetime" || column.Type == "smalldatetime")
                    {
                        resetSearchFormMethod += string.Format("{0}Date.SelectedDate = null;{1}", column.Name, Environment.NewLine);
                    }
                    else if (column.Type == "bit")
                    {
                        resetSearchFormMethod += string.Format("{0}CheckBox.IsChecked = null;{1}", column.Name, Environment.NewLine);
                    }
                    else
                    {
                        resetSearchFormMethod += string.Format("{0}TextBox.Text = string.Empty;{1}", column.Name, Environment.NewLine);
                    }
                }

                template = template.Replace("[ResetSearchFormMethod]", resetSearchFormMethod);
                #endregion

                #region SearchArguments
                string searchArguments = string.Empty;
                string searchVariales = string.Empty;
                foreach (Data.Column column in table.Columns)
                {
                    if (column.Name.ToLower() == "id") continue;

                    if (column.ReferencedTable != null || (column.Type == "tinyint" && column.Description.Contains("نوع")))
                    {
                        searchArguments += string.Format("{0}ComboBox.SelectedIDs,", RemoveEnumFromName(RemoveIDFromName(column.Name)));
                    }
                    else if (column.Type == "datetime" || column.Type == "smalldatetime")
                    {
                        searchArguments += string.Format("{0}Date.SelectedDate,", column.Name);
                    }
                    else if (column.Type == "bit")
                    {
                        searchArguments += string.Format("{0}CheckBox.IsChecked,", column.Name);
                    }
                    else if (column.Type == "int")
                    {
                        searchVariales += string.Format("int {0} = -1;{1}", LowerFirstChar(column.Name), Environment.NewLine);
                        searchVariales += string.Format("if (!string.IsNullOrWhiteSpace({0}TextBox.Text)) {1} = Convert.ToInt32({0}TextBox.Text);{2}{2}", column.Name, LowerFirstChar(column.Name), Environment.NewLine);
                        searchArguments += string.Format("{0},", LowerFirstChar(column.Name));
                    }
                    else if (column.Type == "tinyint")
                    {
                        searchVariales += string.Format("byte {0} = -1;{1}", LowerFirstChar(column.Name), Environment.NewLine);
                        searchVariales += string.Format("if (!string.IsNullOrWhiteSpace({0}TextBox.Text)) {1} = Convert.ToByte({0}TextBox.Text);{2}{2}", column.Name, LowerFirstChar(column.Name), Environment.NewLine);
                        searchArguments += string.Format("{0},", LowerFirstChar(column.Name));
                    }
                    else if (column.Type == "bigint")
                    {
                        searchVariales += string.Format("long {0} = -1;{1}", LowerFirstChar(column.Name), Environment.NewLine);
                        searchVariales += string.Format("if (!string.IsNullOrWhiteSpace({0}TextBox.Text)) {1} = Convert.ToInt64({0}TextBox.Text);{2}{2}", column.Name, LowerFirstChar(column.Name), Environment.NewLine);
                        searchArguments += string.Format("{0},", LowerFirstChar(column.Name));
                    }
                    else 
                    {
                        searchArguments += string.Format("{0}TextBox.Text.Trim(),", column.Name);
                    }
                }

                if (!string.IsNullOrWhiteSpace(searchArguments)) searchArguments = searchArguments.Remove(searchArguments.Length - 1);

                template = template.Replace("[SearchVariales]", searchVariales);
                template = template.Replace("[SearchArguments]", searchArguments);
                #endregion

                return template;
            }
        }

        internal static string GenerateDBCsCode(Data.Table table)
        {
            using (StreamReader reader = new StreamReader("Templates/DB.cs.Template"))
            {
                string template = reader.ReadToEnd();
                template = template.Replace("[TableName]", table.Name);

                #region SearchParameters
                string searchParameters = string.Empty;
                foreach (Data.Column column in table.Columns)
                {
                    if (column.ReferencedTable != null || (column.Type == "tinyint" && column.Description.Contains("نوع")))
                    {
                        searchParameters += string.Format("{0} {1}, ", DBTypeToCSharpType(column.Type), LowerFirstChar(RemoveEnumFromName(RemoveIDFromName(column.Name))));
                    }
                    else if (column.Type == "datetime" || column.Type == "smalldatetime" || column.Type == "bit")
                    {
                        searchParameters += string.Format("{0}? {1}, ", DBTypeToCSharpType(column.Type), LowerFirstChar(column.Name));
                    }
                    else
                    {
                        searchParameters += string.Format("{0} {1}, ", DBTypeToCSharpType(column.Type), LowerFirstChar(column.Name));
                    }
                }

                if (!string.IsNullOrWhiteSpace(searchParameters)) searchParameters = searchParameters.Remove(searchParameters.Length - 2);

                template = template.Replace("[SearchParameters]", searchParameters);
                #endregion

                #region SearchWhereClause
                string searchWhereClause = string.Empty;
                foreach (Data.Column column in table.Columns)
                {
                    if (column.ReferencedTable != null || (column.Type == "tinyint" && column.Description.Contains("نوع")))
                    {
                        searchWhereClause += string.Format("({0}.Count == 0 || {0}.Contains(t.{1}) && {2}", LowerFirstChar(RemoveEnumFromName(RemoveIDFromName(column.Name))), column.Name, Environment.NewLine);
                    }
                    else if (column.Type == "bit" || column.Type.Contains("date"))
                    {
                        searchWhereClause += string.Format("(!{0}.HasValue || t.{1} == {0}) && {2}", LowerFirstChar(column.Name), column.Name, Environment.NewLine);
                    }
                    else if (column.Type.Contains("int"))
                    {
                        searchWhereClause += string.Format("({0} == -1 || t.{1} == {0}) && {2}", LowerFirstChar(column.Name), column.Name, Environment.NewLine);
                    }
                    else
                    {
                        searchWhereClause += string.Format("(string.IsNullOrWhiteSpace({0}) || t.{1}.Contains({0})) && {2}", LowerFirstChar(column.Name), column.Name, Environment.NewLine);
                    }
                }

                if (!string.IsNullOrWhiteSpace(searchWhereClause)) searchWhereClause = searchWhereClause.Remove(searchWhereClause.Length - 6);

                template = template.Replace("[SearchWhereClause]", searchWhereClause);
                #endregion

                return template;
            }
        }

        internal static string GenerateFormXamlCode(Data.Table table)
        {
            using (StreamReader reader = new StreamReader("Templates/Form.xaml.Template"))
            {
                string template = reader.ReadToEnd();
                template = template.Replace("[TableName]", table.Name);

                #region GridItems
                
                string dataGridColumnsItems = string.Empty;
                string gridRows = string.Empty;

               int row = 0;

               foreach (Data.Column column in table.Columns)
               {


                   gridRows += @"<RowDefinition Height=""25"" />" + Environment.NewLine;

                   dataGridColumnsItems += @"<Label Grid.Column=""0"" Grid.Row=""" + row + @""" Content=""" + RemoveEnumFromName(RemoveIDFromName(column.Description)) + @""" />" + System.Environment.NewLine;

                   if (column.ReferencedTable != null && column.Name.ToLower() != "telephoneno")
                   {
                       dataGridColumnsItems += @"<ComboBox  Grid.Column=""1"" Grid.Row=""" + row + @""" x:Name=""" + RemoveIDFromName(column.ReferencedTable.Name) + @"ComboBox"" DisplayMemberPath=""Name"" SelectedValuePath=""ID"" SelectedValue=""{Binding " + column.Name + @"}"" />" + System.Environment.NewLine;
                   }
                   else if (column.Type == "tinyint" && column.Description.Contains("نوع"))
                   {
                       dataGridColumnsItems += @"<ComboBox  Grid.Column=""1"" Grid.Row=""" + row + @""" x:Name=""" + RemoveIDFromName(column.Name) + @"ComboBox"" DisplayMemberPath=""Name"" SelectedValuePath=""ID"" SelectedValue=""{Binding " + column.Name + @"}"" />" + System.Environment.NewLine;
                   }
                   else if (column.Type == "bit")
                   {
                       dataGridColumnsItems += @"<CheckBox Grid.Column=""1"" Grid.Row=""" + row + @""" x:Name=""" + RemoveIDFromName(column.Name) + @"CheckBox"" IsChecked=""{Binding " + column.Name + @"}"" />" + System.Environment.NewLine;
                   }
                   else if (column.Type.ToLower().Contains("date"))
                   {
                       dataGridColumnsItems += @"<cal:DatePicker Grid.Column=""1"" Grid.Row=""" + row + @""" x:Name=""" + RemoveIDFromName(column.Name) + @"TextBox"" SelectedDate=""{Binding " + column.Name + @"}"" />" + System.Environment.NewLine;
                   }
                   else
                   {
                       dataGridColumnsItems += @"<TextBox Grid.Column=""1"" Grid.Row=""" + row + @""" x:Name=""" + RemoveIDFromName(column.Name) + @""" Text=""{Binding " + column.Name + @"}"" />" + System.Environment.NewLine;
                   }

                   row++;
               }

                gridRows = "<Grid.RowDefinitions>" + Environment.NewLine + gridRows + "</Grid.RowDefinitions>" + Environment.NewLine;

                template = template.Replace("[GridItems]", gridRows + dataGridColumnsItems);

                #endregion

                return template;
            }
        }

        internal static string GenerateFormCsCode(Data.Table table)
        {
            using (StreamReader reader = new StreamReader("Templates/Form.cs.Template"))
            {
                string template = reader.ReadToEnd();
                template = template.Replace("[TableName]", table.Name);

                #region InitializeMethod
                string initializeMethodItems = string.Empty;
                foreach (Data.Column column in table.Columns)
                {
                    if (column.ReferencedTable != null && column.Name.ToLower() != "telephoneno")
                    {
                        initializeMethodItems += string.Format("{0}ComboBox.ItemsSource = Data.{0}DB.Get{0}Checkable();{1}", column.ReferencedTable.Name, System.Environment.NewLine);
                    }
                    else if (column.Type == "tinyint" && column.Description.Contains("نوع"))
                    {
                        initializeMethodItems += string.Format("{0}ComboBox.ItemsSource = Helper.GetEnumNameValue(typeof(DB.{0}));{1}", RemoveIDFromName(column.Name), System.Environment.NewLine);
                    }
                }

                template = template.Replace("[InitializeMethod]", initializeMethodItems);
                #endregion

                return template;
            }
        }

        private static string RemoveEnumFromName(string input)
        {
            if (input.IndexOf(':') < 0)
                return input;
            return input.Remove(input.IndexOf(':'));
        }

        private static string RemoveIDFromName(string input)
        {
            if (input.Trim().ToLower() == "id") return input;
            return input.Replace("شناسه", "").Replace("ID", "").Trim();
        }

        private static string LowerFirstChar(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            else if (input.Trim().ToLower() == "id") return "id";
            return input[0].ToString().ToLower() + input.Remove(0, 1);
        }

        private static string DBTypeToCSharpType(string input)
        {
            if (input.Contains("date")) return "DateTime";
            else if (input == "tinyint") return "byte";
            else if (input == "bigint") return "long";
            else if (input == "bit") return "bool";
            else return "string";
        }
    }
}