using ANTLR;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using static ANTLR.LanguageParser;
using Antlr4.Runtime.Tree;

namespace AntlrCSharp
{
	public partial class Compiler : LanguageParserBaseVisitor<object>
	{
		Association _association;
		AssociationEnd _source;
		AssociationEnd _target;

		int _sourceClass = -1;
		int _targetClass = -1;

		public override object VisitAssociation([NotNull] AssociationContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			_association = new();
			_source = new();
			_target = new();
			_association.Line = (uint)context.Start.Line;

			VisitChildren(context);

			_source.ID = (uint)Associations.Count;
			_target.ID = (uint)Associations.Count;

			Classes[_sourceClass]._associationEnds.Add(_source);
			Classes[_targetClass]._associationEnds.Add(_target);

			Associations.Add(_association);
			return null;
		}
		public override object VisitAssociationType([NotNull] AssociationTypeContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			if (context.GetText() != "association") { Errors.Add("At line " + context.Start.Line + ": " + context.GetText() + " unrecognized! Did you mean to write 'association' instead?"); }
			return VisitChildren(context);
		}

        public override object VisitAssociationDefinition([NotNull] AssociationDefinitionContext context)
        {
			var arrows = context.ARROWS();
			if (arrows == null)
			{
				Errors.Add("At line " + context.Start.Line + ": missing arrows for association definition!");
			}
			else 
			{
				if (arrows.GetText() == "")
				{
					Errors.Add("At line " + context.Start.Line + ": missing arrows for association definition!");
				}
				else 
				{
					if (arrows.GetText()[1] == '>')
					{
						_association.IsComposition = true;
					}
					else if (arrows.GetText()[1] == '-') 
					{
						_association.IsComposition = false;
					}
				}
			}
            return base.VisitAssociationDefinition(context);
        }
        public override object VisitAssociationSourceName([NotNull] AssociationSourceNameContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n"); 
			bool found = false;
			foreach (var r in Reserved) 
			{
				if (r == context.GetText()) 
				{ 
					Errors.Add("At line " + context.Start.Line + ": association source role name cannot be '" + r + "'!");
					found = true;
					break;
				}
			}
			if (found == false) 
			{
				foreach (var v in _class._variables)
				{
					if (v.Name == context.GetText()) 
					{ 
						Errors.Add("At line " + context.Start.Line + ": a variable with name '" + v.Name + "' already exists in class '" + _class.ClassName + "'! Check line " + v.Line + "!");
						found = true;
						break;
					}
				}
				if (found == false) 
				{
					foreach (var m in _class._methods)
					{
						if (m.Name == context.GetText())
						{
							Errors.Add("At line " + context.Start.Line + ": a mathod with name '" + m.Name + "' already exists in class '" + _class.ClassName + "'! Check line " + m.Line + "!");
							found = true;
							break;
						}
					}
					if (found == false)
					{
						foreach (var ae in _class._associationEnds)
						{
							if (ae.RoleName == context.GetText())
							{
								Errors.Add("At line " + context.Start.Line + ": an association with role name '" + ae.RoleName + "' already exists in class '" + _class.ClassName + "'! Check line " + Associations[(int)ae.ID].Line + "!");
								found = true;
								break;
							}
						}
						if (found == false)
						{
							if (_class.SuperClass != null)
							{
								foreach (var m in _class.SuperClass._methods)
								{
									if (m.Name == context.GetText())
									{
										Errors.Add("At line " + context.Start.Line + ": a method with name '" + m.Name + "' already exists in super class '" + _class.SuperClass.ClassName + "'! Check line " + m.Line + "!");
										found = true;
										break;
									}
								}
								if (found == false)
								{
									foreach (var v in _class.SuperClass._variables)
									{
										if (v.Name == context.GetText())
										{
											Errors.Add("At line " + context.Start.Line + ": a variable with name '" + v.Name + "' already exists in super class '" + _class.SuperClass.ClassName + "'! Check line " + v.Line + "!");
											found = true;
											break;
										}
									}
									if (found == false)
									{
										foreach (var ae in _class.SuperClass._associationEnds)
										{
											if (ae.RoleName == context.GetText())
											{
												Errors.Add("At line " + context.Start.Line + ": an association with role name '" + ae.RoleName + "' already exists in super class '" + _class.SuperClass.ClassName + "'! Check line " + Associations[(int)ae.ID].Line + "!");
												found = true;
												break;
											}
										}
										if (found == false)
										{
											_association.SourceName = context.GetText();
											_target.RoleName = context.GetText();
										}
									}
								}
							}
							else 
							{
								_association.SourceName = context.GetText();
								_target.RoleName = context.GetText();
							}
						}
					}
				}
			}
			return null;
		}
		public override object VisitSourceClass([NotNull] SourceClassContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n"); 
			bool found = false;
			for (int x=0; x<Classes.Count; x++)
			{
				if (Classes[x].ClassName == context.GetText())
				{
					_association.SourceClass = Classes[x].ClassName;
					_target.ClassName = Classes[x].ClassName;
					_sourceClass = x;
					found = true;
					break;
				}
			}
			if (found == false) { Errors.Add("At line " + context.Start.Line + ": there is no class '" + context.GetText() + "' to use as source class!"); }
			return VisitChildren(context);
		}
		public override object VisitAssociationTargetName([NotNull] AssociationTargetNameContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n"); 
			bool found = false;
			foreach (var r in Reserved)
			{
				if (r == context.GetText())
				{
					Errors.Add("At line " + context.Start.Line + ": association source role name cannot be '" + r + "'!");
					found = true;
					break;
				}
			}
			if (found == false)
			{
				foreach (var v in _class._variables)
				{
					if (v.Name == context.GetText())
					{
						Errors.Add("At line " + context.Start.Line + ": a field with name '" + v.Name + "' already exists in class '" + _class.ClassName + "'! Check line " + v.Line + "!");
						found = true;
						break;
					}
				}
				if (found == false)
				{
					foreach (var m in _class._methods)
					{
						if (m.Name == context.GetText())
						{
							Errors.Add("At line " + context.Start.Line + ": a field with name '" + m.Name + "' already exists in class '" + _class.ClassName + "'! Check line " + m.Line + "!");
							found = true;
							break;
						}
					}
					if (found == false)
					{
						foreach (var ae in _class._associationEnds)
						{
							if (ae.RoleName == context.GetText())
							{
								Errors.Add("At line " + context.Start.Line + ": an association with role name '" + ae.RoleName + "' already exists in class '" + _class.ClassName + "'! Check line " + Associations[(int)ae.ID].Line + "!");
								found = true;
								break;
							}
						}
						if (found == false)
						{
							if (_class.SuperClass != null)
							{
								foreach (var m in _class.SuperClass._methods)
								{
									if (m.Name == context.GetText())
									{
										Errors.Add("At line " + context.Start.Line + ": a field with name '" + m.Name + "' already exists in super class '" + _class.SuperClass.ClassName + "'! Check line " + m.Line + "!");
										found = true;
										break;
									}
								}
								if (found == false)
								{
									foreach (var v in _class.SuperClass._variables)
									{
										if (v.Name == context.GetText())
										{
											Errors.Add("At line " + context.Start.Line + ": a field with name '" + v.Name + "' already exists in super class '" + _class.SuperClass.ClassName + "'! Check line " + v.Line + "!");
											found = true;
											break;
										}
									}
									if (found == false)
									{
										foreach (var ae in _class.SuperClass._associationEnds)
										{
											if (ae.RoleName == context.GetText())
											{
												Errors.Add("At line " + context.Start.Line + ": an association with role name '" + ae.RoleName + "' already exists in super class '" + _class.SuperClass.ClassName + "'! Check line " + Associations[(int)ae.ID].Line + "!");
												found = true;
												break;
											}
										}
										if (found == false)
										{
											_association.TargetName = context.GetText();
											_source.RoleName = context.GetText();
										}
									}
								}
							}
							else 
							{
								_association.TargetName = context.GetText();
								_source.RoleName = context.GetText();
							}
						}
					}
				}
			}
			return null;
		}
        public override object VisitTargetClass([NotNull] TargetClassContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n"); 
			bool found = false;
			for (int x = 0; x < Classes.Count; x++)
			{
				if (Classes[x].ClassName == context.GetText())
				{
					_association.TargetClass = Classes[x].ClassName;
					_source.ClassName = Classes[x].ClassName;
					_targetClass = x;
					found = true;
					break;
				}
			}
			if (found == false) { Errors.Add("At line " + context.Start.Line + ": there is no class '" + context.GetText() + "' to use as target class!"); }
			return VisitChildren(context);
		}
	}
}