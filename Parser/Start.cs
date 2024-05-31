using System.Diagnostics;
using static System.Console;

namespace PSI;

static class Start {
   static void Main () {
      Routine1 ();
      Routine2 ("(3 + 2) * 4 - 17 * -five * (two + 1 + 4 + 5)");
      Routine2 ("3 + 2 > (6 * 5)");
   }

   // Checks 'ExprEvaluator' and 'ExprILGen'
   static void Routine1 () {
      string expr = "(3 + 2) * 4 - 17 * -five * (two + 1 + 4 + 5)";
      var node = new Parser (new Tokenizer (expr)).Parse();

      var dict = new Dictionary<string, int> () { ["five"] = 5, ["two"] = 2 };
      int value = node.Accept (new ExprEvaluator (dict));
      WriteLine ($"Value = {value}");

      var sb = node.Accept (new ExprILGen ());
      WriteLine ("\nGenerated code: ");
      WriteLine (sb);
      Write ("\nPress any key..."); ReadKey (true);
   }

   // Checks the 'ExprGrapher'
   static void Routine2 (string expr) {
      var node = new Parser (new Tokenizer (expr)).Parse();
      var graph = new ExprGrapher (expr);
      node.Accept (graph);

      Directory.CreateDirectory ("c:/etc");
      graph.SaveTo ("c:/etc/test.html");
      var pi = new ProcessStartInfo ("c:/etc/test.html") { UseShellExecute = true };
      Process.Start (pi);
      Write ("\nPress any key..."); ReadKey (true);
   }
}

