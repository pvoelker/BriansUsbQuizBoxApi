using System;
using System.ComponentModel;

namespace BriansUsbQuizBoxApi
{
    /// <summary>
    /// Quiz box enumerations
    /// </summary>
    public enum QuizBoxTypeEnum
    {
        [Description("Brian's Quiz Box")]
        BriansQuizBox = 1,

        [Description("Kirkman Basic Quizbox Plus")]
        KirkmanQuizBox = 2
    }
}
