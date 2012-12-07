using System;
using System.Reflection;
using System.Dynamic.Utils;
using System.Linq;

namespace Android.Dialog
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class EntryAttribute : Attribute
    {
        public string Placeholder;

        public EntryAttribute()
            : this(null)
        {
        }

        public EntryAttribute(string placeholder)
        {
            Placeholder = placeholder;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class DateAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class TimeAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class CheckboxAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class MultilineAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class HtmlAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class SkipAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class StringAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class PasswordAttribute : EntryAttribute
    {
        public PasswordAttribute(string placeholder)
            : base(placeholder)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class AlignmentAttribute : Attribute
    {
        public AlignmentAttribute(object alignment)
        {
            Alignment = alignment;
        }
        public object Alignment;
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class RadioSelectionAttribute : Attribute
    {
        public string Target;

        public RadioSelectionAttribute(string target)
        {
            Target = target;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class OnTapAttribute : Attribute
    {
        public string Method;

        public OnTapAttribute(string method)
        {
            Method = method;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class CaptionAttribute : Attribute
    {
        public string Caption;

        public CaptionAttribute(string caption)
        {
            Caption = caption;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class SectionAttribute : Attribute
    {
        public string Caption, Footer;

        public SectionAttribute()
        {
        }

        public SectionAttribute(string caption)
        {
            Caption = caption;
        }

        public SectionAttribute(string caption, string footer)
        {
            Caption = caption;
            Footer = footer;
        }
    }

    public class RangeAttribute : Attribute
    {
        public int High;
        public int Low;
        public bool ShowCaption;

        public RangeAttribute(int low, int high)
        {
            Low = low;
            High = high;
        }
    }

	/**
	 * The IndexTagsAttribute class, and all references to it throughout this file,
	 * were added by Reinder Kamphorst 2012-12-06.
	 */
	
	/// <summary>
	/// Index tags attribute. Use this attribute to insert the generated 
	/// element into the <seealso cref="BindingContext"/> element index
	/// with given tag. 
	/// </summary>
	/// <remarks>
	/// <para>Usage:</para>
	/// 
	/// <para>In the reflection API, put the following attribute on a field 
	/// or property member:</para>
	/// 
	/// <code>[IndexTags("tag1,tag2")]</code>
	/// 
	/// <para>This inserts the generated element into the element index under 
	/// tags "tag1" and "tag2". If ctx is the binding context, a list 
	/// of elements for one particular tag ("tag1") can be fetched as 
	/// follows:</para>
	/// 
	/// <code>List<Element> elementsForTag1 = ctx.GetElementsForTag("tag1");</code>
	/// 
	/// </remarks>			
	[AttributeUsage (AttributeTargets.Field | AttributeTargets.Property, Inherited=false)]
	public class IndexTagsAttribute : Attribute {
		
		public IndexTagsAttribute(string tags) {
			CsvTags = tags;
		}
		
		public string CsvTags;
		
		public string[] TagList {
			get {
				return CsvTags.Split(',').Select(str => str.Trim().ToLowerInvariant()).ToArray();
			}
		}
	}

	/**
	 * The CustomElementAttribute class, and all references to it throughout this file
	 * and BindingContext.cs, were added by Reinder Kamphorst 2012-12-05.
	 */
	
	/// <summary>
	/// Make your own custom element attributes for the reflection api
	/// by deriving from this class. 
	/// </summary>
	/// <remarks>
	/// For example, if you implement this class as e.g. MyElementAttribute,
	/// specifying how to create an instance of MyElement (CreateElement) and 
	/// specifying how to fetch its value (GetValue), you will be able to
	/// decorate class fields / properties with [MyElement] and have them rendered
	/// through the MonoTouch.Dialog reflection api.
	/// See 
	/// </remarks>
	public abstract class CustomElementAttribute : Attribute {
		/// <summary>
		/// Creates a new element of the type this CustomElementAttribute is made for
		/// </summary>
		/// <returns>
		/// A newly instantiated element that is initialized with given memberValue.
		/// </returns>
		/// <param name='caption>
		/// Caption to display on the created element.
		/// </param>			
		/// <param name='forMember'>
		/// MemberInfo instance reflecting the member to create the element for.
		/// </param>
		/// <param name='memberType'>
		/// Type of the reflected member. 
		/// </param>
		/// <param name='memberValue'>
		/// Value to initialize the new element with. The type of memberValue will be memberType.
		/// </param>
		/// <param name='attributes'>
		/// All the <seealso cref="Attribute"/>s that were defined on this member. 
		/// With these, you can define custon behavior for your element for other attributes,
		/// e.g. <seealso cref="OnTapAttribute"/>.
		/// </param>
		public abstract Element CreateElement(string caption, MemberInfo forMember, Type memberType, object memberValue, object[] attributes);
		
		/// <summary>
		/// Fetch the value of an element that was returned by <see cref="CreateElement"/>.
		/// </summary>
		/// <returns>
		/// The value that is held by given element. 
		/// </returns>
		/// <param name='element'>
		/// Element to fetch the value for. This element was previously created with <see cref="CreateElement"/>.
		/// </param>
		/// <param name='resultType'>
		/// Type of the object to return. Even tough the return type of this method is <see cref="object"/>, 
		/// it is the responsibility of this method to make sure the return value can be
		/// directly cast to the specified resultType (i.e., it should not need extra conversion through,
		/// for example, the <see cref="Convert"/> class).
		/// </param>
		public abstract object GetValue(Element element, Type resultType);
	}
}