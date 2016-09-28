﻿using System;

using DioLive.GaStEn.Engine;
using DioLive.GaStEn.Engine.TicTacToe;

namespace DioLive.GaStEn.Client.TicTacToe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TicTacToeStateMachine engine = new TicTacToeStateMachine();

            ShowState(engine.CurrentState, null);
            var result = engine.Mark('X', 1, 2);
            Console.WriteLine(result.Success);
            //// Console.WriteLine(engine.ProcessMessage(new SetMarkMessage { UserChar = 'X', X = 1, Y = 2 }));

            ShowState(engine.CurrentState, result);
            result = engine.Mark('0', 0, 2);
            Console.WriteLine(result.Success);

            ShowState(engine.CurrentState, result);
            result = engine.Mark('X', 1, 1);
            Console.WriteLine(result.Success);

            ShowState(engine.CurrentState, result);
            result = engine.Mark('0', 0, 1);
            Console.WriteLine(result.Success);

            ShowState(engine.CurrentState, result);
            result = engine.Mark('X', 1, 0);
            Console.WriteLine(result.Success);

            ShowState(engine.CurrentState, result);
        }

        private static void ShowState(State state, ProcessResult result)
        {
            if (result != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(result.StatusMessage);
                Console.ResetColor();
            }

            var playState = state as PlayState;
            if (playState != null)
            {
                ShowPlayState(playState);
                return;
            }

            var drawState = state as DrawState;
            if (drawState != null)
            {
                ShowDrawState(drawState);
                return;
            }

            var winState = state as WinState;
            if (winState != null)
            {
                ShowWinState(winState);
                return;
            }

            Console.WriteLine("Undefined state type: " + state.GetType().Name);
        }

        private static void ShowPlayState(PlayState playState)
        {
            Console.WriteLine("== PlayState ==");
            ShowField(playState.GetField());
        }

        private static void ShowDrawState(DrawState drawState)
        {
            Console.WriteLine("== DrawState ==");
            ShowField(drawState.GetField());
        }

        private static void ShowWinState(WinState winState)
        {
            Console.WriteLine("== WinState ==");
            ShowField(winState.GetField());
            Console.WriteLine("Solution:");
            for (int i = 0; i < winState.FieldSize; i++)
            {
                var point = winState.Solution[i];
                Console.Write($"{point.X}:{point.Y}  -  ");
            }

            Console.WriteLine();
        }

        private static void ShowField(byte[,] field)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Console.Write(field[i, j] + " ");
                }

                Console.WriteLine();
            }
        }
    }
}