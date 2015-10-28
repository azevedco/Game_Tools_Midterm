using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Editor
{
    public partial class Form1 : Form
    {
        
        //List of allowed property types used for populating the combo boxes.
        public List<String> AllowedTypes = new List<string>();
        //List that aliases the fully qualified type names to their common aliases.
        public Dictionary<String, String> AliasTypes = new Dictionary<string, string>() {
            { "System.Boolean", "bool" },
            {"System.Byte", "byte"},
            {"System.SByte", "sbyte"},
            {"System.Char", "char"},
            {"System.Decimal", "decimal"},
            {"System.Double", "double"},
            {"System.Single", "float"},
            {"System.Int32", "int"},
            {"System.UInt32", "uint"},
            {"System.Int64", "long"},
            {"System.UInt64", "ulong"},
            // skip for now. {"System.Object", "object"},
            {"System.Int16", "short"},
            {"System.UInt16", "ushort"},
            {"System.String", "string"},
            {"System.Drawing.Color", "Color"},
            { typeof(Rectangle).ToString(), "Rectangle"}
        };
        public Form1()
        {
            InitializeComponent();
            //Currently there is only a single collection for game entities, which is the 
            // collection of items in the gameEntities_lb listbox. If we are going to have 
            // multiple "levels" in each tab, we need to find a way to separate which entity
            // exists in each tab.

            //Add a blank type to the allowed types list, then copy over
            // all the values from the alias list.
            AllowedTypes.Add("");
            AllowedTypes.AddRange(AliasTypes.Values);

            //Bind the list to each combo box, but using separate binding contextes so they
            // can be selected independently.
            addPropType_cmb.DataSource = AllowedTypes;
            editPropType_cmb.BindingContext = new BindingContext();
            editPropType_cmb.DataSource = AllowedTypes;

            //Setup the tab page events.
            SetTabPanelEvents(tabControl1.TabPages[0]);

        }

        private void SetTabPanelEvents(TabPage tp)
        {
            //Add mouse events
            tp.MouseDown += TabPanel_MouseDown;
            tp.MouseUp += TabPanel_MouseUp;
            tp.MouseClick += TabPanel_MouseClick;
            tp.MouseDoubleClick += TabPanel_MouseDoubleClick;
            tp.MouseMove += TabPanel_MouseMove;
            //Add paint event.
            tp.Paint += TabPanel_Paint;
        }

        
        private void RemoveMouseEvents(TabPage tp)
        {
            //Remove mouse events
            tp.MouseDown -= TabPanel_MouseDown;
            tp.MouseUp -= TabPanel_MouseUp;
            tp.MouseClick -= TabPanel_MouseClick;
            tp.MouseDoubleClick -= TabPanel_MouseDoubleClick;
            tp.MouseMove -= TabPanel_MouseMove;
            //Remove paint events
            tp.Paint -= TabPanel_Paint;
        }
        //Global boolean to check the state of the left mouse button.
        bool LMBDown = false;
        bool Dragging = false;
       
        Point MouseDragOffset = new Point();
        Cursor PreviousCursor = Cursors.Default;
        Point MouseDownPos = new Point();

        private void TabPanel_MouseDown(object sender, MouseEventArgs me)
        {
            MouseDownPos = me.Location;
            if (me.Button == MouseButtons.Left)
            {
                LMBDown = true;
                if (selectedObject_pg.SelectedObject == null)
                {
                    return;
                }
                if(toolsPointer_rb.Checked) {
                    if ((selectedObject_pg.SelectedObject as GameEntity).GetBoundingBox().Contains(me.Location))
                    {
                        Dragging = true;
                        Point bb = (selectedObject_pg.SelectedObject as GameEntity).GetBoundingBox().Location;
                        Point m = me.Location;

                        MouseDragOffset = new Point(m.X - bb.X, m.Y - bb.Y);
                        PreviousCursor = tabControl1.Cursor;
                        tabControl1.Cursor = Cursors.SizeAll;
                    }
                    else {
                        GameEntity ge = (selectedObject_pg.SelectedObject as GameEntity);
                        if (ge != null)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (ge.CornerHandles[i].Contains(me.Location))
                                {
                                    //MIDTERM:
                                    //We are on a corner handle and can start to resize it.
                                }
                            }
                        }

                    }
                }
            }
        }

        private void TabPanel_MouseUp(object sender, MouseEventArgs me)
        {
            if (me.Button == MouseButtons.Left)
            {
                LMBDown = false;
                if (Dragging)
                {
                    Dragging = false;
                    tabControl1.Cursor = PreviousCursor;
                }
            }
        }

        private void TabPanel_MouseMove(object sender, MouseEventArgs me)
        {
            //MIDTERM: This needs to be modified to distinguish between moving an object and 
            // resizing it using the corners.
            if (LMBDown && Dragging)
            {
                GameEntity ge = selectedObject_pg.SelectedObject as GameEntity;
                Rectangle r = ge.GetBoundingBox();
                r.Location = new Point(me.Location.X - MouseDragOffset.X, me.Location.Y - MouseDragOffset.Y);
                ge.SetBoundingBox(r);
                RefreshAll();
            }
        }

        private void TabPanel_MouseClick(object sender, MouseEventArgs me)
        {
            //If this is part of a drag, do nothing.
            Point d = new Point(me.Location.X - MouseDownPos.X, me.Location.Y - MouseDownPos.Y);
            if (Math.Sqrt(d.X * d.X + d.Y * d.Y) > 10.0f)
            {
                RefreshAll();
                return;
            }
            //Check if we are in pointer mode.
            if (toolsPointer_rb.Checked && me.Button == MouseButtons.Left && gameEntities_lb.Items.Count > 0)
            {
                //Get the currently selected entity. This is needed for layering.
                int currIndex = gameEntities_lb.SelectedIndex;
                bool found = false;
                //Iterate through each entity below the currently selected one.
                for (int i = currIndex - 1; i >= 0; i--)
                {
                    GameEntity ge = gameEntities_lb.Items[i] as GameEntity;
                    //If the mouse is within the bounding box.
                    if (ge.GetBoundingBox().Contains(me.Location))
                    {
                        currIndex = i;
                        found = true;
                        break;
                    }
                }
                //If we don't find anything below, check above (including the entity itself)
                if (!found)
                {
                    for (int i = gameEntities_lb.Items.Count - 1; i >= currIndex; i--)
                    {
                        GameEntity ge = gameEntities_lb.Items[i] as GameEntity;
                        //If the mouse is within the bounding box.
                        if (ge.GetBoundingBox().Contains(me.Location))
                        {
                            currIndex = i;
                            found = true;
                            break;
                        }
                    }
                }
                //Set the new index (if one was found) and refresh.
                gameEntities_lb.SelectedIndex = currIndex;
                RefreshAll();
            }
        }

        private void TabPanel_MouseDoubleClick(object sender, MouseEventArgs me)
        {
            //Create a rectangle.
            GameEntity ge = null;
            if (toolsRect_rb.Checked && me.Button == MouseButtons.Left)
            {
                ge = GameEntity.CreateRectangle(me.X, me.Y, 100, 100);
                
            }

            if (ge != null)
            {
                gameEntities_lb.Items.Add(ge);
                gameEntities_lb.SelectedIndex = gameEntities_lb.Items.Count - 1;
                selectedObject_pg.SelectedObject = ge;
                RefreshAll();
            }
        }
        //Custom paint method for displaying the objects. 
        private void TabPanel_Paint(object sender, PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            for (int i = gameEntities_lb.Items.Count - 1; i >= 0; i--)
            {
                GameEntity ge = gameEntities_lb.Items[i] as GameEntity;
                switch (ge.Type)
                {
                    case EntityType.RECT: {
                        Rectangle bb = ge.GetBoundingBox();
                        Color c = (Color)ge.Props["FillColor"];
                        using (SolidBrush sb = new SolidBrush((Color)ge.Props["FillColor"]))
                        {
                            g.FillRectangle(sb, bb);
                        }
                        using (Pen p = new Pen((Color)ge.Props["OutlineColor"]))
                        {
                            g.DrawRectangle(p, bb);
                        }
                        break; }
                    default: {
                        Rectangle bb = ge.GetBoundingBox();
                        using (Pen p = new Pen(Color.Black))
                        {
                            g.DrawRectangle(p, bb);
                        }
                    break; }
                }
                if (gameEntities_lb.SelectedItem == ge)
                {
                    Rectangle outline = ge.OutlineBox;
                    using (Pen p = new Pen(Color.Red))
                    {
                        p.DashPattern = new float[]{4.0f, 2.0f};
                        g.DrawRectangle(p, outline );
                    }
                    using (SolidBrush sb = new SolidBrush(Color.Red))
                    {
                        foreach (Rectangle r in ge.CornerHandles)
                        {
                            g.FillRectangle(sb, r);
                        }
                    }
                }
            }
        }

        //Refresh everything so that the interface stays up to date. The flickering is
        // difficult to fix - you would need to get into double buffering to clean things
        // up. Since this is a tool we don't need to care all that much about the flickering.
        //It is expensive to blindly refresh everything, but again we need to consider that
        // this is a tool - having a big complex algorithm to only refresh the required
        // components isn't worth the time or effort.
        private void RefreshAll()
        {
            gameEntities_lb.Refresh();
            GameEntity ge = gameEntities_lb.SelectedItem as GameEntity;
            selectedObject_pg.SelectedObject = ge;
            selectedObject_pg.Refresh();
            selectedObject_pg.ExpandAllGridItems();
            tabControl1.SelectedTab.Refresh();
        }

        //Trigger when the player changes a grid item (i.e. a property of the selected item).
        private void selectedObject_pg_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            PropertyGrid pg = sender as PropertyGrid;
            if (pg == null)
            {
                RefreshAll();
                return;
            }
            if (pg.SelectedObject == null)
            {
                RefreshAll();
                return;
            }
            Type t = pg.SelectedGridItem.Value.GetType();
            if (t != typeof(CustomPropertyDictionary))
            {
                editPropName_txt.Text = pg.SelectedGridItem.Label;
                String type = pg.SelectedGridItem.Value.GetType().ToString();
                String tout = "";
                editPropType_cmb.Text = AliasTypes.TryGetValue(type, out tout) ? tout : type;
                
            }
            else
            {
                editPropName_txt.Text = "";
                editPropType_cmb.Text = "";
            }
            RefreshAll();
        }

        //Use at your own risk to rename a property.
        private void editProperty_btn_Click(object sender, EventArgs e)
        {
            PropertyGrid pg = selectedObject_pg;
            if (pg.SelectedObject == null)
            {
                return;
            }
            Type t = pg.SelectedGridItem.Value.GetType();
            GameEntity ge = pg.SelectedObject as GameEntity;
            
            if (ge != null && t != typeof(CustomPropertyDictionary))
            {
                if (editPropName_txt.Text != null && editPropName_txt.Text.Trim().Length > 0)
                {
                    ge.Props.TryRename(pg.SelectedGridItem.Label, editPropName_txt.Text);
                    RefreshAll();
                }
            }
        }

        //Use at your own risk to delete a property!
        private void deleteProp_btn_Click(object sender, EventArgs e)
        {
            PropertyGrid pg = selectedObject_pg;
            if (pg.SelectedObject == null)
            {
                return;
            }
            Type t = pg.SelectedGridItem.Value.GetType();
            GameEntity ge = pg.SelectedObject as GameEntity;

            if (ge != null && t != typeof(CustomPropertyDictionary))
            {
                ge.Props.TryDelete(pg.SelectedGridItem.Label);
                RefreshAll();
            }
        }

        //Use at your own risk to add a property. 
        private void addNewProperty_btn_Click(object sender, EventArgs e)
        {
            PropertyGrid pg = selectedObject_pg;
            if (pg.SelectedObject == null)
            {
                return;
            }
            GameEntity ge = pg.SelectedObject as GameEntity;

            if ( ge != null && addPropName_txt.Text != null && addPropName_txt.Text.Trim().Length > 0 )
            {
                Type newType = typeof(System.String);
                if(addPropType_cmb.Text != null && addPropType_cmb.Text.Trim().Length > 0) {
                    try {
                        String t = "System.String";
                        if (AliasTypes.ContainsValue(addPropType_cmb.Text))
                        {
                            foreach (KeyValuePair<string, string> kvp in AliasTypes)
                            {
                                if (kvp.Value == addPropType_cmb.Text)
                                {
                                    t = kvp.Key;
                                    break;
                                }
                            }
                        }
                        newType = Type.GetType(t);
                    } catch {
                        //Do nothing.
                    }
                }
                ge.Props.TryAdd(new CustomProperty { Name = addPropName_txt.Text, Desc = addPropName_txt.Text, Type = newType });
                RefreshAll();
            }
        }

        //Sets the cursor. You can add additional statefulness here or you can
        // use the PreviousCursor and manually set it yourself in the
        // mouse handling methods above.
        private void SetDefaultTabControlCursor()
        {
            if (toolsPointer_rb.Checked)
            {
                tabControl1.Cursor = Cursors.Arrow;
            }
            else if (toolsRect_rb.Checked)
            {
                tabControl1.Cursor = Cursors.Cross;
            }
        }

        //Whenever the user changes the tool, change the cursor to the appropriate type.
        private void ToolRadioButtons_CheckedChanged(object sender, EventArgs e)
        {
            SetDefaultTabControlCursor();
        }

        private void gameEntities_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void selectedObject_pg_Click(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void selectedObject_pg_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            RefreshAll();
        }
        //Export the current level to XML.
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Entities");
            
            foreach (GameEntity ge in gameEntities_lb.Items)
            {
                XmlElement el = ge.GenerateXML(doc);
                root.AppendChild(el);
            }
            doc.AppendChild(root);
            doc.Save("Output.xml");
        }

        
    }
}
