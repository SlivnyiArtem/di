﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using TagsCloudVisualization.FileReader;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.Layouter.Normalizer;
using TagsCloudVisualization.TextAnalization;
using TagsCloudVisualization.TextAnalization.Analyzer;
using TagsCloudVisualization.TextAnalization.VisualizatorMaker;

namespace TagsCloudVisualization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var path = "C:/GitHub/di/img_words.jpeg";
            var arguments = "-lndw";
            var filePath = "C:/GitHub/di/TagsCloudVisualization/War_and_piece_words.Docx";
            var savePath =
                "C:/GitHub/di/TagsCloudVisualization/result.TXT";
            var mystemPath = "C:\\GitHub\\di\\TagsCloudVisualization\\mystem.exe";



            Process process = Process.Start(new ProcessStartInfo
            {
                FileName = mystemPath,
                Arguments = arguments + ' ' + filePath + ' ' + savePath,
            });

            
                //КЛИЕНТЫ




            var containerBuilder = new ContainerBuilder();
            InitializeRegistration(containerBuilder);
            var buildContainer = containerBuilder.Build();

            var reader = buildContainer.Resolve<ITextFileReader>();
            var wordsFromFile = reader.ReadText(savePath, Encoding.UTF8);



            var analyzer = buildContainer.Resolve<IAnalyzer>();
            var analyzedWords = analyzer.GetAnalyzedWords(wordsFromFile).ToList();
            //Здесь анализ на часть слова и соответствие строки слову
            //ИСКЛЮЧИ СКУЧНЫЕ СЛОВА


            var normalizer = buildContainer.Resolve<IWordNormalizer>();
            var normalyzedWords = NormalyzeWords(analyzedWords, normalizer).ToList();
            //Здесь расширение функционала нормализации



            var rectangleSize = new Size(100, 100);






            var filler = buildContainer.Resolve<IContentFiller>();
            



            



            


            //НЕСКОЛЬКО АЛГОРИТМОВ
            filler.FillInElements(rectangleSize, normalyzedWords);

            var elementsForVisualisation = filler.GetElementsList();



            //ниже добавить параметризацию цветов и другихз настроек
            using (var visualization = new Visualization(elementsForVisualisation, new Pen(Color.White, 10),
                new SolidBrush(Color.White), new Font("Times", 15)))
            {
                visualization.DrawAndSaveImage(new Size(5000, 5000), path, ImageFormat.Jpeg);
            }
        }

        private static IEnumerable<Word> NormalyzeWords(IEnumerable<Word> analyzedWords, IWordNormalizer normalizer)
        {
            foreach (var word in analyzedWords)
                yield return normalizer.NormalizeWord(word);
        }

        private static void InitializeRegistration(ContainerBuilder buildContainer)
        {
            RegistrationOfTextFileReader(buildContainer);
            RegistrationOfLayouter(buildContainer);
            RegistrationOfVisualizator(buildContainer);
            RegistrationOfTextAnalyzer(buildContainer);
            RegistrationOfNormalizer(buildContainer);
        }

        private static void RegistrationOfNormalizer(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<WordNormalizerOrigin>().As<IWordNormalizer>();
        }

        private static void RegistrationOfVisualizator(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<VisualizatorMaker>().As<IVisualizatorMaker>();
        }

        private static void RegistrationOfLayouter(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<CircularCloudLayouterForRectanglesWithText>().As<ICircularCloudLayouter, IContentFiller>()
                .WithParameter("center", new Point(2500, 2500));
        }

        private static void RegistrationOfTextAnalyzer(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<Analyzer>().As<IAnalyzer>()
                .WithParameter("speechParts",
                    Enum.GetValues(typeof(PartsOfSpeech.SpeechPart)).Cast<PartsOfSpeech.SpeechPart>());
        }

        private static void RegistrationOfTextFileReader(ContainerBuilder buildContainer)
        {
            buildContainer.RegisterType<TextFileReader>().As<ITextFileReader>();
        }
    }
}
