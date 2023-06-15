public struct FloatUpper0point1
{
    private float val;
    private FloatUpper0point1(float _val)
    {
        if (_val < 0.1f)
        {
            //반드시 0.1f이상의 값을 유지
            val = 0.1f;
        }
        else
        {
            val = _val;
        }
    }
    public static implicit operator float(FloatUpper0point1 _val)
    {
        return _val.val;
    }
    public static implicit operator FloatUpper0point1(float _val)
    {
        return new FloatUpper0point1(_val);
    }
}
