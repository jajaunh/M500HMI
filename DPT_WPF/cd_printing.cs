using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DPT_WPF
{
    public class Facts
    {
        public float layerHeight;
        public List<int> _dataNumber1 = new List<int>();
        public List<int> _dataNumber3 = new List<int>();
        public List<string> _dataposition1 = new List<string>();
        public List<string> _dataposition3 = new List<string>();
        public List<string> _dataString1 = new List<string>();
        public List<string> _dataString3 = new List<string>();
    }
    public partial class HMIM270
    {
        string filePath = "";
        float layerthickness;
        string dataNumber = "";
        int layercount = 0;
        string sss = "";




        List<BinaryReader> bReader = new List<BinaryReader>();
        List<Facts> _facts = new List<Facts>();

        public string Header { get; private set; }

        #region GetData1
        public void GetData(BinaryReader reader)
        {

            UInt32 nHeader = ReadUInt32(reader);
            UInt32 nHeaderSize = ReadUInt32(reader);
            UInt32 nFixedHeader = ReadUInt32(reader);
            UInt32 nFixedHeaderSize = ReadUInt32(reader);
            this.Header = System.Text.Encoding.ASCII.GetString(reader.ReadBytes(Convert.ToInt32(nFixedHeaderSize))).Trim();

            UInt32 nVersionInfo = ReadUInt32(reader);
            UInt32 nVersionSize = ReadUInt32(reader);
            UInt32 nVersionMajor = ReadUInt32(reader);
            UInt32 nVersionMinor = ReadUInt32(reader);

            UInt32 nVersionName = ReadUInt32(reader);
            UInt32 nVersionNameSize = ReadUInt32(reader);
            this.Header = System.Text.Encoding.ASCII.GetString(reader.ReadBytes(Convert.ToInt32(nVersionNameSize))).Trim();

            UInt32 nAllLayer = ReadUInt32(reader);
            UInt32 nAllLayerSize = ReadUInt32(reader);
            GetData2(reader);
        }
        #endregion

        #region GetData2
        private void GetData2(BinaryReader reader)
        {

            while (true)
            {
                if (reader.BaseStream.Position != reader.BaseStream.Length)
                {

                }
                else
                {
                    break;
                }

                UInt32 tempNum = ReadUInt32(reader);

                // 레이어 시작
                // 21 - 15 00 00 00 
                if (tempNum == 21)
                {
                    UInt32 nLayerInfoSize = ReadUInt32(reader); //79 00 00 00
                }


                else if (tempNum == 210)
                {
                    Facts f = new Facts();
                    //UInt32 nlayer = ReadUInt32(reader); //d2 00 00 00
                    UInt32 nlayerSize = ReadUInt32(reader); //04 00 00 00
                    float nlayerHeight = ReadFloat(reader); //0a d7 a3 bc
                    layerthickness = nlayerHeight;

                    while (true)
                    {
                        if (reader.BaseStream.Position != reader.BaseStream.Length)
                        {

                        }
                        else
                        {
                            _facts.Add(f);
                            break;
                        }

                        UInt32 check0 = ReadUInt32(reader);

                        //d4
                        if (check0 == 212)
                        {

                            UInt32 datablockSize = ReadUInt32(reader);

                            //2120
                            UInt32 udatablockType = ReadUInt32(reader);
                            UInt32 udatablockTypeSize = ReadUInt32(reader);
                            var udatablockTypeNumber = reader.ReadBytes(1);
                            dataNumber = Convert.ToString(udatablockTypeNumber[0]);

                            // 2121 - 49 08 00 00
                            UInt32 part_identi = ReadUInt32(reader);
                            var part_identiSize = ReadUInt32(reader);
                            var part_identiNumber = ReadUInt32(reader);

                            // 2122 - 4a 08 00 00
                            UInt32 com_identi = ReadUInt32(reader);
                            var com_identiSize = ReadUInt32(reader);
                            var com_identiNumber = ReadUInt32(reader);

                            // 2123 - 4b 08 00 00
                            UInt32 pro_identi = ReadUInt32(reader);
                            var pro_identiSize = ReadUInt32(reader);
                            var pro_identiNumber = ReadUInt32(reader);

                            // 2124 - 4c 08 00 00
                            UInt32 pro_identii = ReadUInt32(reader);
                            if (dataNumber == "1")
                            {
                                uint ucountSize = ReadUInt32(reader);
                                int uucount = Convert.ToInt32(ucountSize);

                                f._dataNumber1.Add(1);
                                f._dataposition1.Add(Convert.ToString(reader.BaseStream.Position));
                                f._dataString1.Add(System.Text.Encoding.ASCII.GetString(reader.ReadBytes(uucount)));
                                f.layerHeight = layerthickness;

                            }
                            else if (dataNumber == "3")
                            {
                                uint ucountSize = ReadUInt32(reader);
                                int uucount = Convert.ToInt32(ucountSize);

                                f._dataNumber3.Add(3);
                                f._dataposition3.Add(Convert.ToString(reader.BaseStream.Position));
                                f._dataString3.Add(System.Text.Encoding.ASCII.GetString(reader.ReadBytes(uucount)));
                                f.layerHeight = layerthickness;

                            }
                            else
                            {
                                MessageBox.Show("여기는 데이터블럭이 없습니다.");
                            }
                        }
                        else//(check0==)
                        {
                            //uint ucountSize = ReadUInt32(reader);
                            uint ucountSize1 = ReadUInt32(reader);
                            _facts.Add(f);
                            break;
                        }
                        // 2120 - 48 08 00 00

                    }
                    //                _facts.Add(f);
                    layercount = layercount + 1;
                }

                else
                {
                    uint ucont = ReadUInt32(reader);
                    MessageBox.Show("여기는 데이터가 없습니다.");
                    break;
                }


            }

            allmodelLayer = layercount;
            layercount = 0;
            allmodelingCount++;
        }

        #endregion


        #region int 형태로 변환하기
        private static uint ReadUInt32(BinaryReader reader)
        {
            var bytes = reader.ReadBytes(4);
            return BitConverter.ToUInt32(bytes, 0);
        }
        #endregion

        #region float 형태로 변환하기
        private static float ReadFloat(BinaryReader reader)
        {
            var bytes = reader.ReadBytes(4);
            return BitConverter.ToSingle(bytes, 0);
        }
        #endregion


        private double totalLine = 0;
        private double spotsize = 0.05;
        private double layerHeight = 0.03;
        Line line;
        GeometryGroup lineGeometryGroup = new GeometryGroup();
        GeometryGroup outlineGeometryGroup = new GeometryGroup();
        int transValue = 0;
        private float beforeX1 = 0;
        private float beforeY1 = 0;
        private float beforeX = 0;
        private float beforeY = 0;
        int tempvalue2 = 0;
        PointCollection collection = new PointCollection();

        #region loading
        private void loading()
        {
            try
            {
                //totalLine = 0;
                #region
                myCanvas.Children.Clear();
                line = new Line();
                lineGeometryGroup.Children.Clear();
                outlineGeometryGroup.Children.Clear();

                for (int g = 0; g < allFile.Count; g++)
                {
                    Stream inStream = new FileStream(sss + "\\" + allFile[g], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    var reader1 = new BinaryReader(inStream);

                    if (_facts[showlayer]._dataNumber1.Count > 0)
                    {
                        foreach (string dp in _facts[showlayer]._dataposition1)
                        {
                            reader1.BaseStream.Position = Convert.ToInt64(dp) - 4;
                            uint ucont = ReadUInt32(reader1) / 8;
                            for (int i = 0; i < ucont; i++)
                            {
                                LineGeometry innerLineGeometry = new LineGeometry();
                                float pointX = ReadFloat(reader1);
                                float pointY = ReadFloat(reader1);

                                if (transValue == 1)
                                {
                                    innerLineGeometry.StartPoint = new Point(beforeX1 * 8.5, beforeY1 * 8.5);
                                    innerLineGeometry.EndPoint = new Point(pointX * 8.5, pointY * 8.5);
                                    lineGeometryGroup.Children.Add(innerLineGeometry);
                                    //totalLine = totalLine + Math.Sqrt(Math.Pow((pointX) - (beforeX1), 2) + Math.Pow((pointY) - (beforeY1), 2));

                                    transValue = 0;
                                }
                                else
                                {
                                    transValue = 1;
                                }
                                beforeX1 = pointX;
                                beforeY1 = pointY;
                            }
                        }
                    }



                    if (_facts[showlayer]._dataNumber3.Count > 0)
                    {
                        foreach (string dp in _facts[showlayer]._dataposition3)
                        {

                            reader1.BaseStream.Position = Convert.ToInt64(dp);
                            while (true)
                            {
                                uint ucont = ReadUInt32(reader1);
                                //reader1 = calculate(Convert.ToInt32(ucont), reader1);
                                //int dad = 0;

                                if (ucont == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    float tempcount = _facts[showlayer].layerHeight;
                                    reader1 = calculate(Convert.ToInt32(ucont), reader1);
                                }
                            }
                        }
                    }

                    showlayer = showlayer + allmodelLayer;

                    inStream.Close();
                }


                showlayer = 0;


                //double alltotal = totalLine * spotsize * layerHeight;

                //MessageBox.Show(Convert.ToString(alltotal));



                Polyline pline = new Polyline();
                pline.Points = collection;
                pline.Stroke = new SolidColorBrush(Colors.Red);
                pline.StrokeThickness = 1;
                myCanvas.Children.Add(pline);

                SolidColorBrush blackBrush = new SolidColorBrush();
                blackBrush.Color = Colors.Blue;
                System.Windows.Shapes.Path linePath = new System.Windows.Shapes.Path();
                linePath.Stroke = blackBrush;
                linePath.StrokeThickness = 1;
                linePath.Data = lineGeometryGroup;
                myCanvas.Children.Add(linePath);

                SolidColorBrush outlineBrush = new SolidColorBrush();
                outlineBrush.Color = Colors.Red;
                System.Windows.Shapes.Path outlinePath = new System.Windows.Shapes.Path();
                outlinePath.Stroke = outlineBrush;
                outlinePath.StrokeThickness = 1;
                outlinePath.Data = outlineGeometryGroup;
                myCanvas.Children.Add(outlinePath);

            }
            catch (Exception e)
            {

            }


            #endregion
        }
        #endregion

        private BinaryReader calculate(int ucont, BinaryReader reader1)
        {
            tempvalue2 = 3;

            for (int j = 0; j < ucont; j++)
            {
                float pointX = ReadFloat(reader1);
                float pointY = ReadFloat(reader1);



                if (tempvalue2 == 1)
                {
                    LineGeometry innerLineGeometry = new LineGeometry();
                    innerLineGeometry.StartPoint = new Point(beforeX * 8.5, beforeY * 8.5);
                    innerLineGeometry.EndPoint = new Point(pointX * 8.5, pointY * 8.5);
                    outlineGeometryGroup.Children.Add(innerLineGeometry);
                    tempvalue2 = 0;
                }
                else if (tempvalue2 == 3)
                {
                    LineGeometry innerLineGeometry = new LineGeometry();
                    innerLineGeometry.StartPoint = new Point(beforeX * 8.5, beforeY * 8.5);
                    innerLineGeometry.EndPoint = new Point(pointX * 8.5, pointY * 8.5);
                    tempvalue2 = 0;
                }

                else
                {
                    LineGeometry innerLineGeometry = new LineGeometry();
                    innerLineGeometry.StartPoint = new Point(beforeX * 8.5, beforeY * 8.5);
                    innerLineGeometry.EndPoint = new Point(pointX * 8.5, pointY * 8.5);
                    outlineGeometryGroup.Children.Add(innerLineGeometry);
                    tempvalue2 = 1;
                }

                beforeX = pointX;
                beforeY = pointY;
            }
            return reader1;
        }
    }
}
