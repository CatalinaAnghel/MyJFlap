using JFlap_version5.Commands;
using JFlap_version5.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace JFlap_version5.ViewModels
{
    class StateViewModel
    {
        #region Variables
        private int nextIndex;
        private string action;
        public MyTextBox Txt { get; set; }
        private State state1, state2;
        private Arrow arrow;
        private List<State> States = new List<State>();
        private List<Arrow> Arrows = new List<Arrow>();
        private bool _isMoving;
        private Line line;
        private State movingState;
        private Grid movingGrid;
        private List<Tuple<UIElement, string>> UndoStack = new List<Tuple<UIElement, string>>();
        private List<Tuple<UIElement, string>> RedoStack = new List<Tuple<UIElement, string>>();
        #endregion

        #region Properties
        public Canvas MyCanvas { get; private set; }
        public Command CreateCommand { get; private set; }
        public Command MouseDownAddCommand { get; private set; }
        public Command MouseDownConnectCommand { get; private set; }
        public Command MouseUpConnectCommand { get; private set; }
        public Command SelectCommand { get; private set; }
        public Command DeleteCommand { get; private set; }
        public Command ConnectCommand { get; private set; }
        public Command UndoCommand { get; private set; }
        public Command RedoCommand { get; private set; }
        #endregion

        #region Constructor
        public StateViewModel()
        {
            action = "create";
            nextIndex = 0;
            CreateCommand = new Command(ChangeActionToCreate, CanChangeAction);
            DeleteCommand = new Command(ChangeActionToDelete, CanChangeAction);
            SelectCommand = new Command(ChangeActionToSelect, CanChangeAction);
            ConnectCommand = new Command(ChangeActionToConnect, CanChangeAction);
            UndoCommand = new Command(Undo, CanUndo);
            RedoCommand = new Command(Redo, CanRedo);
            MouseDownAddCommand = new Command(AddStateOnCanvas, CanAddStateOnCanvas);
            MouseDownConnectCommand = new Command(SelectFirstStateToConnectOnCanvas, CanConnectStatesOnCanvas);
            MouseUpConnectCommand = new Command(SelectSecondStateToConnectOnCanvas, CanConnectStatesOnCanvas);

            MyCanvas = new Canvas();
            MyCanvas.MouseDown += MyCanvas_MouseDown;
            MyCanvas.MouseMove += MyCanvas_MouseMove;
            MyCanvas.MouseUp += MyCanvas_MouseUp;
            movingGrid = null;
            nextIndex = 0;
            Txt = new MyTextBox();
            state1 = null;
            state2 = null;
            movingState = null;
            arrow = new Arrow();
            _isMoving = false;
            line = null;
        }
        #endregion

        #region Commands and Events

        #region Redo Command
        public bool CanRedo(object parameter)
        {
            return RedoStack.Count > 0;
        }

        public void Redo(object parameter)
        {
            if(RedoStack.Count > 0)
            {
                var tuple = RedoStack.Last();
                // It is not finished.
                switch (tuple.Item2)
                {
                    case "create":
                        if (tuple.Item1.GetType().Equals(typeof(Grid)))
                        {
                            MyCanvas.Children.Add(tuple.Item1);
                            UndoStack.Add(tuple);
                            RedoStack.Remove(tuple);
                            Txt.NumberOfStates++;
                        }
                        break;
                    case "delete":
                        if (tuple.Item1.GetType().Equals(typeof(Grid)))
                        {
                            MyCanvas.Children.Remove(tuple.Item1);
                            Txt.NumberOfStates--;
                            UndoStack.Add(tuple);
                            RedoStack.Remove(tuple);
                        }
                        else
                        {
                            MyCanvas.Children.Remove(tuple.Item1);
                            RedoStack.Remove(tuple);
                            UndoStack.Add(tuple);
                            var next = RedoStack.Last();
                            MyCanvas.Children.Remove(next.Item1);
                            RedoStack.Remove(next);
                            UndoStack.Add(next);
                        }

                        break;
                    case "connect":
                        MyCanvas.Children.Add(tuple.Item1);
                        RedoStack.Remove(tuple);
                        var nextTuple = RedoStack.Last();
                        MyCanvas.Children.Add(nextTuple.Item1);
                        RedoStack.Remove(nextTuple);
                        UndoStack.Add(tuple);
                        UndoStack.Add(nextTuple);
                        break;
                    case "select":
                        Console.WriteLine("Here, we have the select action");
                        break;
                }
            }
            
        }
        #endregion

        #region Undo Command
        public bool CanUndo(object parameter)
        {
            return UndoStack.Count > 0;
        }
        public void Undo(object parameter)
        {
            if(UndoStack.Count > 0)
            {
                var tuple = UndoStack.Last();
                // It is not finished.
                switch (tuple.Item2)
                {
                    case "create":
                        if (tuple.Item1.GetType().Equals(typeof(Grid)))
                        {
                            MyCanvas.Children.Remove(tuple.Item1);
                            Txt.NumberOfStates--;
                            RedoStack.Add(tuple);
                            UndoStack.Remove(tuple);
                        }
                        break;
                    case "delete":
                        if (tuple.Item1.GetType().Equals(typeof(Grid)))
                        {
                            MyCanvas.Children.Add(tuple.Item1);
                            Txt.NumberOfStates++;
                            RedoStack.Add(tuple);
                            UndoStack.Remove(tuple);
                        }
                        else
                        {
                            MyCanvas.Children.Add(tuple.Item1);
                            RedoStack.Add(tuple);
                            UndoStack.Remove(tuple);
                            var next = UndoStack.Last();
                            MyCanvas.Children.Add(next.Item1);
                            RedoStack.Add(next);
                            UndoStack.Remove(next);
                        }  
                        break;
                    case "connect":
                        Console.WriteLine(tuple.Item1.ToString());
                        MyCanvas.Children.Remove(tuple.Item1);
                        UndoStack.Remove(tuple);
                        var nextTuple = UndoStack.Last();
                        Console.WriteLine(nextTuple);
                        MyCanvas.Children.Remove(nextTuple.Item1);
                        UndoStack.Remove(nextTuple);
                        RedoStack.Add(tuple);
                        RedoStack.Add(nextTuple);
                        break;
                    case "select":
                        Console.WriteLine("Here, we have the select action");
                        if (tuple.Item1.GetType().Equals(typeof(Grid)))
                        {
                            MyCanvas.Children.Remove(tuple.Item1);
                            RedoStack.Add(tuple);
                            UndoStack.Remove(tuple);
                            var last = UndoStack.Last();
                            if (last.Item2.Equals("before select"))
                            {
                                MyCanvas.Children.Add(last.Item1);
                                UndoStack.Remove(last);
                                RedoStack.Add(last);
                            }

                        }
                        break;
                }
            }
            
        }
        #endregion

        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (action.Equals("connect") && (state1 != null) && _isMoving)
            {
                if (line != null)
                {
                    MyCanvas.Children.Remove(line);
                    line = null;
                }

                line = new Line();
                line.X1 = state1.X + 50;
                line.Y1 = state1.Y + 25;
                Console.WriteLine(Mouse.GetPosition(MyCanvas));
                Point point = Mouse.GetPosition(MyCanvas);
                line.X2 = point.X - 5;
                line.Y2 = point.Y;
                line.StrokeThickness = 2;
                line.Stroke = Brushes.Black;
                MyCanvas.Children.Add(line);
                MyCanvas.CaptureMouse();
            }
            else if (action.Equals("select") && movingState != null)
            {
                MyCanvas.Children.Remove(movingGrid as UIElement);
                Point mousePos = Mouse.GetPosition(MyCanvas);
                Console.WriteLine("Position: " + mousePos.ToString());
                State state = new State
                {
                    X = mousePos.X - 25,
                    Y = mousePos.Y - 25,
                    Width = 50,
                    Height = 50,
                    Id = Int32.Parse(movingState.Content.Substring(1))
                };

                States.Add(state);

                Ellipse stateel = new Ellipse
                {
                    Height = 50,
                    Width = 50,
                    Fill = Brushes.LightBlue,
                    Stroke = Brushes.Black
                };

                Label lbl = new Label
                {
                    Content = "S" + Int32.Parse(movingState.Content.Substring(1)),
                    Height = 50,
                    Width = 50,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center
                };

                Grid stateGrid = new Grid();
                stateGrid.Children.Add(stateel);
                stateGrid.Children.Add(lbl);
                stateGrid.Width = 50;
                stateGrid.Height = 50;
                stateGrid.Cursor = Cursors.Hand;
                Canvas.SetLeft(stateGrid, state.X);
                Canvas.SetTop(stateGrid, state.Y);
                MyCanvas.Children.Add(stateGrid);
                movingGrid = stateGrid;
                MyCanvas.CaptureMouse();
            }
        }    
        

        private void MyCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Point point = e.GetPosition((UIElement)sender);

                // Perform the hit test against a given portion of the visual object tree.
                HitTestResult result = VisualTreeHelper.HitTest(MyCanvas, point);

                if (action.Equals("connect"))
                {
                    Console.WriteLine(point.X);
                    Console.WriteLine(point.Y);

                    if (result != null)
                    {
                        // Get the label for the result
                        Label label = FindParent<Label>(result.VisualHit);

                        // Search the state that has the specific label
                        foreach (var state in States)
                        {
                            if (state.Content.Equals(label.Content))
                            {
                                // set the second state
                                state2 = state;
                            }
                        }

                        // delete the line
                        if (line != null)
                        {
                            MyCanvas.Children.Remove(line);
                            line = null;
                        }

                        if (state1 != state2)
                        {
                            if(state1.X < state2.X)
                            {
                                // create a new line
                                line = new Line
                                {
                                    /* set the coordonates for the new line(this is the final line,
                                        it will connect the states)*/
                                    X1 = state1.X + 50,
                                    Y1 = state1.Y + 25
                                };

                                line.X2 = Math.Abs(state2.X);
                                line.Y2 = Math.Abs(state2.Y + state2.Height / 2);
                            }
                            else
                            {
                                Console.WriteLine("state 1: " + state1.ToString());
                                Console.WriteLine("state 2: " + state2.ToString());
                                // create a new line
                                line = new Line
                                {
                                    Name = state1.Content.Concat(state2.Content).ToString(),
                                    /* set the coordonates for the new line(this is the final line,
                                        it will connect the states)*/
                                    X1 = state1.X,
                                    Y1 = state1.Y + state1.Height/2
                                };

                                line.X2 = state2.X + state2.Width/2;
                                line.Y2 = state2.Y;
                            }
                            

                            line.StrokeThickness = 2;
                            line.Stroke = Brushes.Black;
                            line.Cursor = Cursors.Cross;
                            MyCanvas.Children.Add(line);
                            Tuple<UIElement, string> t = new Tuple<UIElement, string>(line, action);
                            Console.WriteLine("Before: " + UndoStack.Count);
                            UndoStack.Add(t);
                            TextBox txt = new TextBox
                            {
                                Text = "Transition",
                                Name = line.Name
                            };
                            
                            Canvas.SetLeft(txt, state1.X + (state2.X - state1.X) / 2);
                            Canvas.SetTop(txt, state1.Y + (state2.Y - state1.Y) / 2+ 5);
                            MyCanvas.Children.Add(txt);
                            Tuple<UIElement, string> t2 = new Tuple<UIElement, string>(txt, action);
                            UndoStack.Add(t2);
                            Console.WriteLine("After: " + UndoStack.Count);
                            Arrow newArrow = new Arrow
                            {
                                Label = txt,
                                Line = line
                            };
                            Arrows.Add(newArrow);
                            line = null;
                            
                        }
                        else
                        {
                            // draw an arc from a state to the same state
                            var g = new StreamGeometry();

                            using (var gc = g.Open())
                            {
                                gc.BeginFigure(
                                    startPoint: new Point(state1.X + 20, state1.Y),
                                    isFilled: false,
                                    isClosed: false);

                                gc.ArcTo(
                                    point: new Point(state1.X + 30, state1.Y),
                                    size: new Size(3, 10),
                                    rotationAngle: 0d,
                                    isLargeArc: false,
                                    sweepDirection: SweepDirection.Clockwise,
                                    isStroked: true,
                                    isSmoothJoin: true);
                            }

                            var path = new Path
                            {
                                Stroke = Brushes.Black,
                                StrokeThickness = 2,
                                Data = g
                            };

                            MyCanvas.Children.Add(path);
                            Tuple<UIElement, string> t = new Tuple<UIElement, string>(path, action);
                            UndoStack.Add(t);
                            TextBox txt = new TextBox
                            {
                                Text = "Transition"
                            };
                            Canvas.SetLeft(txt, state1.X);
                            Canvas.SetTop(txt, state1.Y - state1.Height*3/4);
                            MyCanvas.Children.Add(txt);
                            Tuple<UIElement, string> t2 = new Tuple<UIElement, string>(txt, action);
                            UndoStack.Add(t2);
                        }

                        state1 = null;
                        state2 = null;
                        _isMoving = false;
                        MyCanvas.ReleaseMouseCapture();
                    }
                }
                else if(action.Equals("select")){
                    
                    MyCanvas.Children.Remove(movingGrid as UIElement);
                    Point mousePos = Mouse.GetPosition(MyCanvas);
                    Console.WriteLine("Position: " + mousePos.ToString());
                    State state = new State
                    {
                        X = mousePos.X - 25,
                        Y = mousePos.Y - 25,
                        Width = 50,
                        Height = 50,
                        Id = Int32.Parse(movingState.Content.Substring(1))
                    };

                    States.Add(state);

                    Ellipse stateel = new Ellipse
                    {
                        Height = 50,
                        Width = 50,
                        Fill = Brushes.LightBlue,
                        Stroke = Brushes.Black
                    };

                    Label lbl = new Label
                    {
                        Content = "S" + Int32.Parse(movingState.Content.Substring(1)),
                        Height = 50,
                        Width = 50,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center
                    };
                    
                    Grid stateGrid = new Grid();
                    stateGrid.Children.Add(stateel);
                    stateGrid.Children.Add(lbl);
                    stateGrid.Width = 50;
                    stateGrid.Height = 50;
                    stateGrid.Cursor = Cursors.Hand;
                    Canvas.SetLeft(stateGrid, state.X);
                    Canvas.SetTop(stateGrid, state.Y);
                    
                    movingState = null;
                    movingGrid = null;
                    _isMoving = false;
                    MyCanvas.ReleaseMouseCapture();
                    MyCanvas.Children.Add(stateGrid);
                    foreach (var el in UndoStack)
                    {
                        if (el.GetType().Equals(typeof(Grid)) && ((el.Item1 as Grid).Children[1] as Label).Content.Equals(lbl.Content))
                        {
                            UndoStack.Remove(el);
                            RedoStack.Add(el);
                        }
                    }

                    for(var index = MyCanvas.Children.Count - 1; index >= 0; index--)
                    {
                        if(MyCanvas.Children[index].GetType().Equals(typeof(Line)) && (MyCanvas.Children[index] as Line).Name.Contains((stateGrid.Children[1] as Label).Content.ToString()))
                        {
                            //MyCanvas.Children[1]
                            //Console.WriteLine("fjsdhfd");
                        }
                    }
                    
                    Tuple<UIElement, string> t = new Tuple<UIElement, string>(stateGrid, action);
                    UndoStack.Add(t);
                    
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Something is wrong...");
            }
            
        }

        private bool CanAddStateOnCanvas(object parameter)
        {
            if (action.Equals("create"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool CanChangeAction(object parameter)
        {
            return true;
        }

        private void ChangeActionToCreate(object parameter)
        {
            this.action = "create";
            Console.WriteLine(action);
        }

        private void ChangeActionToSelect(object parameter)
        {
            this.action = "select";
            Console.WriteLine(action);

        }

        private void ChangeActionToDelete(object parameter)
        {
            this.action = "delete";
            Console.WriteLine(action);
        }

        private void ChangeActionToConnect(object parameter)
        {
            this.action = "connect";
            Console.WriteLine(action);
        }

        private void AddStateOnCanvas(object parameter)
        {
            if (action.Equals("create"))
            {
                Point mousePos = Mouse.GetPosition((IInputElement)parameter);
                Console.WriteLine("Position: " + mousePos.ToString());
                State state = new State
                {
                    X = mousePos.X - 45,
                    Y = mousePos.Y - 75,
                    Width = 50,
                    Height = 50,
                    Id = nextIndex
                };

                States.Add(state);
                
                Txt.NumberOfStates++;

                Ellipse stateel = new Ellipse
                {
                    Height = 50,
                    Width = 50,
                    Fill = Brushes.LightBlue,
                    Stroke = Brushes.Black
                };

                Label lbl = new Label
                {
                    Content = "S" + nextIndex,
                    Height = 50,
                    Width = 50,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center
                };

                nextIndex++;

                Grid stateGrid = new Grid();
                stateGrid.Children.Add(stateel);
                stateGrid.Children.Add(lbl);
                stateGrid.Width = 50;
                stateGrid.Height = 50;
                stateGrid.Cursor = Cursors.Hand;
                Canvas.SetLeft(stateGrid, state.X);
                Canvas.SetTop(stateGrid, state.Y);
                MyCanvas.Children.Add(stateGrid);
                Tuple<UIElement, string> t = new Tuple<UIElement, string>(stateGrid, action);
                UndoStack.Add(t);
            }
            
        }

        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Point point = e.GetPosition((UIElement)sender);

                // Perform the hit test against a given portion of the visual object tree.
                HitTestResult result = VisualTreeHelper.HitTest(MyCanvas, point);
                Console.WriteLine(result);
                switch (action)
                {
                    case "delete":
                        Console.WriteLine(point.X);
                        Console.WriteLine(point.Y);

                        if (result != null)
                        {
                            Label label = FindParent<Label>(result.VisualHit);
                            if (label != null)
                            {
                                UIElement el = null;
                                for (int inx = MyCanvas.Children.Count - 1; inx >= 0; inx--)
                                {
                                    Console.WriteLine(inx);
                                    Console.WriteLine("Here:");
                                    el = MyCanvas.Children[inx];
                                    Console.WriteLine(label.Content.ToString());
                                    Console.WriteLine("Type:" + el.GetType().ToString());
                                    if (el.GetType().Equals(typeof(Line)) && (el as Line).Name.Contains(label.Content.ToString()))
                                    {
                                        Console.WriteLine(el);
                                        MyCanvas.Children.Remove(el);
                                    }
                                }
                                MyCanvas.Children.Remove(label.Parent as UIElement);
                                Txt.NumberOfStates--;
                                Tuple<UIElement, string> t = new Tuple<UIElement, string>(label.Parent as UIElement, action);

                                UndoStack.Add(t);
                            }
                            else
                            {
                                foreach (var arrow in Arrows)
                                {
                                    if (arrow.Line == (result.VisualHit as Line)
                                        || arrow.Label == (result.VisualHit as TextBox))
                                    {
                                        MyCanvas.Children.Remove(result.VisualHit as UIElement);
                                        Tuple<UIElement, string> arrowTuple = new Tuple<UIElement, string>(result.VisualHit as Line, action);
                                        UndoStack.Add(arrowTuple);
                                        MyCanvas.Children.Remove(arrow.Label);
                                        Tuple<UIElement, string> t = new Tuple<UIElement, string>(arrow.Label as UIElement, action);

                                        UndoStack.Add(t);
                                    }
                                }

                            }

                        }
                        break;
                    case "connect":
                        if (state1 == null)
                        {
                            Console.WriteLine(point.X);
                            Console.WriteLine(point.Y);

                            if (result != null)
                            {
                                Label label = FindParent<Label>(result.VisualHit);
                                Console.WriteLine(label.Content);
                                foreach (var state in States)
                                {
                                    if (state.Content.Equals(label.Content))
                                    {
                                        state1 = state;
                                    }
                                }
                            }
                            _isMoving = true;
                        }
                        break;
                    case "select":
                        if (result != null)
                        {
                            Console.WriteLine(result.VisualHit.ToString());
                            Label label = FindParent<Label>(result.VisualHit);
                            Console.WriteLine(label.Content);
                            foreach (var state in States)
                            {
                                if (state.Content.Equals(label.Content))
                                {
                                    movingState = state;
                                    bool found = false;
                                    foreach (var child in MyCanvas.Children)
                                    {
                                        
                                        if ((child as Grid).Children[1].ToString().Split(' ')[1].Equals(label.Content))
                                        {
                                            Console.WriteLine((child as Grid).Children[1].ToString());
                                            movingGrid = child as Grid;
                                            found = true;
                                            Tuple<UIElement, string> tuple = new Tuple<UIElement, string>(movingGrid, "before select");
                                            UndoStack.Add(tuple);
                                            
                                        }
                                        if (found)
                                        {
                                            continue;
                                        }


                                    }
                                    found = false;
                                }
                            }
                        }
                        _isMoving = true;
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong...");
            }
            
        }

        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            //we have reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }

        private bool CanConnectStatesOnCanvas(object parameter)
        {
            if (action.Equals("delete"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SelectFirstStateToConnectOnCanvas(object parameter)
        {
            arrow.State1 = parameter as State;
        }
        private void SelectSecondStateToConnectOnCanvas(object parameter)
        {
            arrow.State2 = parameter as State;
        }

    }
    #endregion
}