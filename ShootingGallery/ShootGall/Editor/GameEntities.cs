using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Editor
{
    //Game Entity class. Uses a variety of dynamic techniques to avoid having to use a lot
    // of inheritance.
    //Contains a properties dictionary you can use to stick whatever you want into it.
    //If the entity is drawable, you should give it a bounding box, which is used for mouse
    // interactions and on screen drawing. Things like dragging the handles should alter the
    // bounding box, and the specific implementation of how that effects the game entity type
    // should be implemented in the delegate function for that type of bounding box.
    public class GameEntity
    {
        //Each entity should have an immutable, unique ID.
        private static int sNextID = 0;
        public int ID { get; private set; }

        //Bounding box delegates. You need to define a custom version of
        // each of these to use them. The bounding box is the smallest axis
        // aligned rectangle that the entity fits in on the screen - for a rectangle
        // this is the same size as a rectangle, but it would be different for a 
        // circle. Bounding box is used for resizing and picking the entity without
        // caring what type it is.
        public delegate void delSetBoundingBox(Rectangle r);
        public delSetBoundingBox SetBoundingBox;
        public delegate Rectangle delGetBoundingBox();
        public delGetBoundingBox GetBoundingBox;

        //The list of properties. Customizable for each entity.
        private CustomPropertyDictionary m_props = new CustomPropertyDictionary();
        public CustomPropertyDictionary Props
        {
            get { return m_props; }
        }
        //The outline box is an Axis Aligned Bounding box that is slightly
        // bigger than the bounding box. It is used when you draw a box around
        // a selected game entity in the editor.
        private const int OUTLINE_PADDING = 4;
        [Browsable(false)]
        public Rectangle OutlineBox
        {
            get
            {
                Rectangle r = GetBoundingBox();
                return new Rectangle(r.X - OUTLINE_PADDING, r.Y - OUTLINE_PADDING, 
                    r.Width + (2 * OUTLINE_PADDING), r.Height + (2 * OUTLINE_PADDING)
                );
            }
        }

        private const int HALFHANDLESIZE = 3;
        private const int HANDLESIZE = HALFHANDLESIZE * 2;
        [Browsable(false)]
        public Rectangle[] CornerHandles
        {
            get
            {
                Rectangle outline = OutlineBox;
                return new Rectangle[] {
                    new Rectangle(outline.X - HALFHANDLESIZE, outline.Y - HALFHANDLESIZE, HANDLESIZE, HANDLESIZE),
                    new Rectangle(outline.X + outline.Width - HALFHANDLESIZE, outline.Y - HALFHANDLESIZE, HANDLESIZE, HANDLESIZE),
                    new Rectangle(outline.X + outline.Width - HALFHANDLESIZE, outline.Y + outline.Height - HALFHANDLESIZE, HANDLESIZE, HANDLESIZE),
                    new Rectangle(outline.X - HALFHANDLESIZE, outline.Y + outline.Height - HALFHANDLESIZE, HANDLESIZE, HANDLESIZE)
                };
            }
        }

        //Type of entity. The integer value should be consist in the game world.
        public EntityType Type { get; private set; }

        //Used for the listbox that display entities.
        public override string ToString()
        {
            return this.Type.ToString() + " " + ID;
        }

        //Constructor that ensures a unique ID. Add a new constructor at your peril.
        private GameEntity()
        {
            ID = sNextID++;
        }

        //Factory method for creating a rectangular game entity. This can be chained with other
        // factory methods to create new entity types - for example a sprite game entity would
        // create a rectangle entity first, then have additional logic to add a texture, subrect, etc...
        public static GameEntity CreateRectangle(int x, int y, int w, int h)
        {
            GameEntity ge = new GameEntity();
            ge.Type = EntityType.RECT;
            ge.Props.TryAdd(new CustomProperty { Name = "OutlineColor", Type = typeof(Color), DefaultValue = Color.Black });
            ge.Props.TryAdd(new CustomProperty { Name = "FillColor", Type = typeof(Color), DefaultValue = Color.Transparent });
            CustomProperty dim = new CustomProperty { Name = "Dimensions", Type = typeof(Rectangle), DefaultValue = new Rectangle(x,y,w,h) };
            ge.Props.TryAdd(dim);
            //ge.Props.TryAdd(new CustomProperty { Name = "DynamicProperties", Type = typeof(CustomPropertyDictionary), DefaultValue = new CustomPropertyDictionary() });
            
            ge.SetBoundingBox = new delSetBoundingBox(delegate(Rectangle r)
            {
                ge.Props["Dimensions"] = r;
            });

            ge.GetBoundingBox = new delGetBoundingBox(delegate()
            {
                return ge.Props["Dimensions"] == null ? (Rectangle)dim.DefaultValue : (Rectangle)ge.Props["Dimensions"];
            });

               
            return ge;
        }

        public static GameEntity CreateCircle(int radius, Point position)
        {
            GameEntity ge = new GameEntity();
            ge.Type = EntityType.CIRCLE;

            //MIDTERM: Initialise the rest of the entity, including the properties (see CreateRectangle() for reference).
            // Don't forget you also need to initialise the BoundingBox delegates.
            ge.Props.TryAdd(new CustomProperty { Name = "OutlineColor", Type = typeof(Color), DefaultValue = Color.Black });
            ge.Props.TryAdd(new CustomProperty { Name = "FillColor", Type = typeof(Color), DefaultValue = Color.Transparent });

            /* Creating these as named variables so that they can be referred to later. */
            CustomProperty rad = new CustomProperty { Name = "Radius", Type = typeof(int), DefaultValue = radius };
            ge.Props.TryAdd(rad);
            CustomProperty pos = new CustomProperty { Name = "Position", Type = typeof(Point), DefaultValue = new Point(position.X, position.Y) };
            ge.Props.TryAdd(pos);

            ge.SetBoundingBox = new delSetBoundingBox(delegate (Rectangle r)
            {
                ge.Props["Position"] = r.Location;

                /* Only height or width can be used, averaging them makes resizing difficult */
                ge.Props["Radius"] = r.Size.Width / 2;
            });

            ge.GetBoundingBox = new delGetBoundingBox(delegate ()
            {
                /* Generate a bounding box based on the radius. */
                Rectangle bound;
                if (ge.Props["Position"] == null || ge.Props["Radius"] == null)
                {
                    bound = new Rectangle((Point)pos.DefaultValue, new Size((int)rad.DefaultValue*2, (int)rad.DefaultValue*2));
                }
                else
                {
                    bound = new Rectangle((Point)ge.Props["Position"], new Size((int)ge.Props["Radius"]*2, (int)ge.Props["Radius"]*2));
                }
                return bound;
            });

            return ge;
        }

        // Take in a GameEntity, create new GameEntity, set new entity with new values equal to original GameEntity
        public static GameEntity CloneEntity(GameEntity ge)
        {
            GameEntity newGE = new GameEntity();

            int clonePosOffset = 6;

            newGE.Type = ge.Type;

            newGE.Props.TryAdd(new CustomProperty { Name = "OutlineColor", Type = typeof(Color), DefaultValue = (Color)ge.Props["OutlineColor"] });
            newGE.Props.TryAdd(new CustomProperty { Name = "FillColor", Type = typeof(Color), DefaultValue = (Color)ge.Props["FillColor"] });

            switch(newGE.Type)
            {
                case EntityType.CIRCLE:
                    CustomProperty rad = new CustomProperty { Name = "Radius", Type = typeof(int), DefaultValue = (int)ge.Props["Radius"] };
                    newGE.Props.TryAdd(rad);
                    Point gePos = (Point)ge.Props["Position"];
                    gePos.X += clonePosOffset;
                    gePos.Y += clonePosOffset;
                    CustomProperty pos = new CustomProperty { Name = "Position", Type = typeof(Point), DefaultValue = gePos };
                    newGE.Props.TryAdd(pos);

                    newGE.SetBoundingBox = new delSetBoundingBox(delegate(Rectangle r)
                    {
                        newGE.Props["Position"] = r.Location;

                        /* Only height or width can be used, averaging them makes resizing difficult */
                        newGE.Props["Radius"] = r.Size.Width;
                    });

                    newGE.GetBoundingBox = new delGetBoundingBox(delegate()
                    {
                        /* Generate a bounding box based on the radius. */
                        Rectangle bound;
                        if (newGE.Props["Position"] == null || newGE.Props["Radius"] == null)
                        {
                            bound = new Rectangle((Point)pos.DefaultValue, new Size((int)rad.DefaultValue, (int)rad.DefaultValue));
                        }
                        else
                        {
                            bound = new Rectangle((Point)newGE.Props["Position"], new Size((int)newGE.Props["Radius"], (int)newGE.Props["Radius"]));
                        }
                        return bound;
                    });

                    break;
                case EntityType.RECT:
                    Rectangle geRect = (Rectangle)ge.Props["Dimensions"];
                    geRect.X += clonePosOffset;
                    geRect.Y += clonePosOffset;
                    CustomProperty dim = new CustomProperty { Name = "Dimensions", Type = typeof(Rectangle), DefaultValue = geRect };
                    newGE.Props.TryAdd(dim);

                    newGE.SetBoundingBox = new delSetBoundingBox(delegate(Rectangle r)
                    {
                        newGE.Props["Dimensions"] = r;
                    });

                    newGE.GetBoundingBox = new delGetBoundingBox(delegate()
                    {
                        return newGE.Props["Dimensions"] == null ? (Rectangle)dim.DefaultValue : (Rectangle)newGE.Props["Dimensions"];
                    });

                    break;
                default:
                    return null;
            }

            return newGE;
        }

        //Output this game entity as an XMLElement. The Entity is the element and will contain
        // each property as a sub-element. In addition, the entity will contain attributes for the
        // Type (as an int), Type (as a string), and ID.
        public XmlElement GenerateXML(XmlDocument doc)
        {
            //Output the XML. Note that this doesn't care what type of entity is being outputted.
            XmlElement el = doc.CreateElement("GameEntity");
            XmlAttribute tatt = doc.CreateAttribute("Type");
            tatt.Value = ((int)this.Type).ToString();
            //The string name is useful for debugging.
            XmlAttribute tsatt = doc.CreateAttribute("TypeStr");
            tsatt.Value = this.Type.ToString();
            XmlAttribute idatt = doc.CreateAttribute("ID");
            idatt.Value = this.ID.ToString();
            el.Attributes.Append(tatt);
            el.Attributes.Append(tsatt);
            el.Attributes.Append(idatt);
            this.Props.ToXml(el, doc);
            return el;
        }
    }
}
