using System.Collections;
using System.Globalization;

namespace WebApi.Test.InLineData;
public class CultureInLineDataTest : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "pt-BR" };
        yield return new object[] { "en" };
        yield return new object[] { "zh-CN" };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
