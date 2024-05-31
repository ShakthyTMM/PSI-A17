namespace PSI;

class ExprGrapher : Visitor<int> {
   public ExprGrapher (string expression) => mExp = expression;

   public override int Visit (NLiteral literal) {
      mSB.AppendLine ($"id{++mID}[{literal.Value.Text}]");
      return mID;
   }

   public override int Visit (NIdentifier identifier) {
      mSB.AppendLine ($"id{++mID}[{identifier.Name.Text}]");
      return mID;
   }

   public override int Visit (NUnary unary) {
      int a = unary.Expr.Accept (this);
      mSB.AppendLine ($"id{++mID}([{unary.Op.Text}]); id{mID} --> id{a}");
      return mID;
   }

   public override int Visit (NBinary binary) {
      int a = binary.Left.Accept (this); int b = binary.Right.Accept (this);
      mSB.AppendLine ($"id{++mID}([{binary.Op.Text}]); id{mID} --> id{a}; id{mID} --> id{b}");
      return mID;
   }

   public void SaveTo (string file) {
      string text = $$"""
         <!DOCTYPE html>
         <head><meta charset="utf-8"></head>
         <body>
           Graph of {{mExp}}
           <pre class="mermaid">
             graph TD
             {{mSB}}
           </pre> 
           <script type="module">
             import mermaid from 'https://cdn.jsdelivr.net/npm/mermaid@10/dist/mermaid.esm.min.mjs';
             mermaid.initialize({ startOnLoad: true });
           </script>  
         </body>
         """;
      File.WriteAllText (file, text);
   }

   readonly StringBuilder mSB = new ();
   readonly string mExp = "";
   int mID;
}
