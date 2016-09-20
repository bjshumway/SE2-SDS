public class Title {
    private string _text;
    private bool _beforeName;

    public string text { // the actual text of the title (ex: Dr.)
        get {
            return _text;
        }
    }

    public bool beforeName { // does the title come before the name? (Mr. Irwin vs. Collen the ANNIHILATOR)
        get {
            return _beforeName;
        }
    }

    public Title(string text, bool beforeName) {
        _text = text;
        _beforeName = beforeName;
    }
}
