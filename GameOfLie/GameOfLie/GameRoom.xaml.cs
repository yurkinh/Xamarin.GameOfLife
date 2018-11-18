﻿using GameOfLie.Logic;
using SkiaSharp;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameOfLie
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameRoom : ContentPage
	{
        public SKPaint DeadPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Black
        };
        public SKPaint AlivePaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.WhiteSmoke
        };


        private bool[,] _state;
        //private bool[,] _state = new bool[,]
        //{
        //    {false,false, false, false,false },
        //    {false,false, true, false,false },
        //    {false,false, true, false,false },
        //    {false,false, true, false,false },
        //    {false,false, false, false,false },
        //};

        private Matrix _matrix;
            
        public GameRoom ()
		{
			InitializeComponent();

            var xLength = 75;
            var yLength = 125;
            _state = Matrix.GenerateInitialState(xLength, yLength);
            _matrix = new Matrix(_state);
            Device.StartTimer(TimeSpan.FromSeconds(10f/60), () =>
            {
                _state = _matrix.Next();
                canvasView.InvalidateSurface();
                return true; // True = Repeat again, False = Stop the timer
            });
        }

        private void CanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear(SKColors.Black);

            var height = e.Info.Height;
            var width = e.Info.Width;

            var rows = _state.GetLength(0);
            var columns = _state.GetLength(1);


            var rectHeight = height/ rows;
            var rectWidth = width / columns;


            for (int i = 0; i < _state.GetLength(0); i++)
            {
                for (int j = 0; j < _state.GetLength(1); j++)
                {
                    if(_state[i, j])
                    {
                        //alive
                        //canvas.DrawRect(rectWidth * j, rectHeight * i, rectWidth, rectHeight, AlivePaint);
                        canvas.DrawCircle(rectWidth * j, rectHeight * i, rectHeight / 5, AlivePaint);
                    }
                    else
                    {
                        //dead 
                        canvas.DrawRect(rectWidth * j, rectHeight * i, rectWidth, rectHeight, DeadPaint);
                    }
                }
            }

        }
    }
}