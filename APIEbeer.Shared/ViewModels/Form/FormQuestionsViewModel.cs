﻿namespace APIEbeer.Shared.ViewModels.Form
{
    public class FormQuestionsViewModel
    {
        public required string Characteristic { get; set; }
        public required string Question { get; set; }
        public required List<string> Options { get; set; }
    }
}
