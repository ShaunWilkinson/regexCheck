namespace SPandCRM.Xrm.regexMatch
{
    using System;
    using System.Activities;
    using System.Text.RegularExpressions;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Workflow;

    public class RegexMatch : CodeActivity
    {
        [RequiredArgument]
        [Input("String to Search")]
        public InArgument<string> searchString { get; set; }

        [RequiredArgument]
        [Input("Regex")]
        public InArgument<string> regexString { get; set; }

        [OutputAttribute("Regex Match")]
        public OutArgument<bool> regexMatch { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            ITracingService tracingService = context.GetExtension<ITracingService>();

            try
            {
                Boolean regexMatched = false;
                string inputTxt = searchString.Get<string>(context);
                Regex r = new Regex(regexString.Get<string>(context), RegexOptions.IgnoreCase);

                // Perform the match
                Match m = r.Match(inputTxt);
                regexMatched = m.Success;

                // Set output
                regexMatch.Set(context, regexMatched);

            }
            catch (Exception e)
            {
                tracingService.Trace("Exception in Regex Match: {0}", e.ToString());
            }
        }
    }
}
