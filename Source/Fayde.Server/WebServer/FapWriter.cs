﻿using System;
using System.IO;
using Fayde.Core;
using Fayde.Xaml;

namespace Fayde.WebServer
{
    public class FapWriter : IDisposable
    {
        private static readonly string START_INITIALIZATION_SCRIPT =
@"      <script type=""text/javascript"">
        Fayde.Run = function () {";

        private static readonly string END_INITIALIZATION_SCRIPT =
@"          Fayde.Start(appType, theme, rjson, json, document.getElementById(""canvas""));
        }
        </script>";

        public FapWriter(Stream outputStream)
        {
            Writer = new StreamWriter(outputStream);
        }

        public void Dispose()
        {
            if (Writer != null)
            {
                Writer.Dispose();
                Writer = null;
            }
        }

        protected StreamWriter Writer { get; set; }

        public void WriteStart()
        {
            Writer.WriteLine("<!DOCTYPE html>");
            Writer.WriteLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        }
        public void WriteEnd()
        {
            Writer.WriteLine("</html>");
        }

        public void WriteHeadStart()
        {
            Writer.WriteLine("\t<head>");
        }
        public void WriteHeadEnd()
        {
            Writer.WriteLine("\t</head>");
        }

        public void WriteTitle(string title)
        {
            Writer.WriteLine(string.Format("\t\t<title>{0}</title>", title));
        }

        public void WriteScriptIncludes(string scriptResolution, string theme)
        {
            Writer.WriteLine(string.Format("\t\t<script src=\"{0}Fayde.js\" type=\"text/javascript\"></script>", scriptResolution));
            Writer.WriteLine(string.Format("\t\t<script src=\"{0}Fayde.Theme.{1}.js\" type=\"text/javascript\"></script>", scriptResolution, theme));
        }

        public void WriteScriptInclude(string scriptPath)
        {
            Writer.WriteLine(string.Format("\t\t<script src=\"{0}\" type=\"text/javascript\"></script>", scriptPath));
        }

        public void WriteAppLoadScript(FaydeApplication fap)
        {
            var outputMods = new JsonOutputModifiers { IsNamespaceIncluded = true };

            string rjson = "{}";
            if (fap.Resources != null)
                rjson = fap.Resources.ToJson(0, outputMods);

            string json = "{}";
            if (fap.Content != null)
                json = fap.Content.ToJson(0, outputMods);

            Writer.WriteLine(START_INITIALIZATION_SCRIPT);
            Writer.WriteLine(string.Format("var theme = \"{0}\";", fap.Theme));
            Writer.WriteLine(string.Format("var appType = {0};", fap.GetTypeName(outputMods)));
            Writer.WriteLine(string.Format("var rjson = {0};", rjson));
            Writer.WriteLine(string.Format("var json = {0};", json));
            Writer.WriteLine(END_INITIALIZATION_SCRIPT);
        }

        public void WriteBodyStart()
        {
            Writer.WriteLine("\t<body onload=\"Fayde.Run()\" onmousedown=\"return false;\" style=\"margin: 0;\">");
        }
        public void WriteBodyEnd()
        {
            Writer.WriteLine("\t</body>");
        }

        public void WriteCanvas()
        {
            Writer.WriteLine("\t\t<canvas id=\"canvas\" tabindex=\"1\" style=\"position: absolute;\"></canvas>");
        }
    }
}