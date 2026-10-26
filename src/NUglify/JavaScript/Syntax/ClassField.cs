using System.Collections.Generic;
using NUglify.JavaScript.Visitors;

namespace NUglify.JavaScript.Syntax
{
    public class ClassField : AstNode
    {
	    BindingIdentifier binding;
	    ArrayLiteral computedName;
	    AstNode initializer;
	    
        public bool IsStatic { get; set; }

        public SourceContext StaticContext { get; set; }

        public BindingIdentifier Binding
        {
            get => binding;
            set => ReplaceNode(ref binding, value);
        }
        public string Name { get; set; }
        public ArrayLiteral ComputedName
        {
            get => computedName;
            set => ReplaceNode(ref computedName, value);
        }

        public override bool IsDeclaration => false;

        public ClassField(SourceContext functionContext)
            : base(functionContext)
        {
        }

        public override void Accept(IVisitor visitor)
        {
            if (visitor != null)
	            visitor.Visit(this);
        }

        public bool IsReferenced => true;
        public AstNode Initializer
        {
            get => initializer;
            set => ReplaceNode(ref initializer, value);
        }

        public override IEnumerable<AstNode> Children => EnumerateNonNullNodes(binding, computedName, initializer);

        public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
        {
            if (Binding == oldNode)
            {
                Binding = newNode as BindingIdentifier;
                return true;
            }

            if (ComputedName == oldNode)
            {
                ComputedName = newNode as ArrayLiteral;
                return true;
            }

            if (Initializer == oldNode)
            {
                Initializer = newNode;
                return true;
            }

            return false;
        }
    }
}
