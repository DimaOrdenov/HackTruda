using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace HackTruda.Behaviors
{
    public class MaskedBehavior : Behavior<Entry>
    {
        private IDictionary<int, char> _positions;

        public static readonly BindableProperty MaskTemplateProperty = BindableProperty.Create(
          nameof(MaskTemplate),
          typeof(string),
          typeof(MaskedBehavior),
          propertyChanged: OnMaskTemplatePropertyChanged,
          defaultBindingMode: BindingMode.TwoWay);

        public string MaskTemplate
        {
            get => (string)GetValue(MaskTemplateProperty);
            set => SetValue(MaskTemplateProperty, value);
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnMaskTemplatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((MaskedBehavior)bindable).SetPositions();
        }

        private void SetPositions()
        {
            if (string.IsNullOrEmpty(MaskTemplate))
            {
                _positions = null;
                return;
            }

            var list = new Dictionary<int, char>();
            for (var i = 0; i < MaskTemplate.Length; i++)
            {
                if (MaskTemplate[i] != 'X')
                {
                    list.Add(i, MaskTemplate[i]);
                }
            }

            _positions = list;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = sender as Entry;

            var text = entry.Text;

            if (_positions == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            if (text.Length > MaskTemplate.Length)
            {
                entry.Text = text.Remove(text.Length - 1);
                return;
            }

            foreach (var position in _positions)
            {
                if (text.Length >= position.Key + 1)
                {
                    var value = position.Value.ToString();

                    if (text.Substring(position.Key, 1) != value)
                    {
                        text = text.Insert(position.Key, value);
                    }
                }
            }

            if (entry.Text != text)
            {
                entry.Text = text;
            }
        }
    }
}
