namespace Utilities.Test.Maybe;

using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

public class MaybeTestSuite
{
    [Test]
    public async ValueTask Maybe_Some_ShouldCreateSome()
    {
        await Assert.That(Maybe.Some(Guid.NewGuid().ToString()).IsSome).IsTrue();
    }

    [Test]
    public async ValueTask Maybe_None_ShouldCreateNone()
    {
        Maybe<string> maybe = Maybe.None;

        await Assert.That(maybe.IsNone).IsTrue();
    }

    [Test]
    public async ValueTask Maybe_IsSome_ShouldReturnTrueForSome()
    {
        var maybe = Maybe.Some(Guid.NewGuid().ToString());

        using (Assert.Multiple())
        {
            await Assert.That(maybe.IsSome).IsTrue();
            await Assert.That(maybe.IsNone).IsFalse();
        }
    }

    [Test]
    public async ValueTask Maybe_IsNone_ShouldReturnTrueForNone()
    {
        Maybe<string> maybe = Maybe.None;

        using (Assert.Multiple())
        {
            await Assert.That(maybe.IsSome).IsFalse();
            await Assert.That(maybe.IsNone).IsTrue();
        }
    }

    [Test]
    public async ValueTask Maybe_ToMaybe_ShouldCreateSomeFromNonNullReferenceType()
    {
        var maybe = Guid.NewGuid().ToString().ToMaybe();

        await Assert.That(maybe.IsSome).IsTrue();
    }

    [Test]
    public async ValueTask Maybe_ToMaybe_ShouldCreateNoneFromNullReferenceType()
    {
        var maybe = ((string)null).ToMaybe();

        await Assert.That(maybe.IsNone).IsTrue();
    }

    [Test]
    public async ValueTask Maybe_ToMaybe_ShouldCreateSomeFromNullableTypeWithValue()
    {
        int? value = 1;
        var maybe = value.ToMaybe();

        await Assert.That(maybe.IsSome).IsTrue();
    }

    [Test]
    public async ValueTask Maybe_ToMaybe_ShouldCreateNoneFromNullableTypeWithoutValue()
    {
        int? value = default;
        var maybe = value.ToMaybe();

        await Assert.That(maybe.IsNone).IsTrue();
    }

    [Test]
    public async ValueTask Maybe_ToNullable_ShouldCreateNullableWithValueFromSome()
    {
        await Assert.That(Maybe.Some(1).ToNullable().HasValue).IsTrue();
    }

    [Test]
    public async ValueTask Maybe_ToNullable_ShouldCreateNullableWithoutValueFromNone()
    {
        await Assert.That(default(Maybe<int>).ToNullable().HasValue).IsFalse();
    }

    [Test]
    public async ValueTask Maybe_Flatten_ShouldReturnSomeForInnerSome()
    {
        var inner = Maybe.Some(Guid.NewGuid().ToString());

        await Assert.That(Maybe.Some(inner).Flatten()).IsEqualTo(inner);
    }

    [Test]
    public async ValueTask Maybe_Flatten_ShouldReturnNoneForInnerNone()
    {
        await Assert.That(default(Maybe<Maybe<string>>).Flatten()).IsEqualTo(Maybe.None);
    }

    [Test]
    public async ValueTask Maybe_GetValues_ShouldValidateParameters()
    {
        await Assert.That(() => ((List<Maybe<string>>)null).GetValues().ToList()).Throws<ArgumentNullException>();
    }

    [Test]
    public async ValueTask Maybe_GetValues_ShouldReturnValuesForSomeCases()
    {
        var list = new List<Maybe<string>>
        {
            Maybe.Some(Guid.NewGuid().ToString()),
            Maybe.Some(Guid.NewGuid().ToString()),
        };

        await Assert.That(list.GetValues().Count()).IsEqualTo(2);
    }

    [Test]
    public async ValueTask Maybe_GetValues_ShouldReturnEmptyForNoneCases()
    {
        var list = new List<Maybe<string>> { Maybe.None, Maybe.None };

        await Assert.That(list.GetValues().Count()).IsEqualTo(0);
    }

    [Test]
    public async ValueTask Maybe_GetValues_ShouldReturnValuesFromMixedCases()
    {
        var list = new List<Maybe<string>>
        {
            Maybe.Some("Test1"),
            Maybe.None,
            Maybe.Some("Test2"),
            Maybe.None,
            Maybe.Some(Guid.NewGuid().ToString()),
        };

        await Assert.That(list.GetValues().Count()).IsEqualTo(3);
    }

    [Test]
    public async ValueTask Maybe_GetHashCode_ShouldReturnUnderlyingHashCodeForSome()
    {
        var test = Guid.NewGuid().ToString();

        await Assert.That(Maybe.Some(test).GetHashCode()).IsEqualTo(test.GetHashCode());
    }

    [Test]
    public async ValueTask Maybe_GetHashCode_ShouldReturnZeroForNone()
    {
        await Assert.That(default(Maybe<string>).GetHashCode()).IsEqualTo(0);
    }

    [Test]
    public async ValueTask Maybe_Expect_ShouldThrowWithTheProvidedMessageOnNone()
    {
        Maybe<string> maybe = Maybe.None;
        var message = Guid.NewGuid().ToString();

        using (Assert.Multiple())
        {
            var exception = await Assert.That(() => maybe.Expect(message)).Throws<InvalidOperationException>();
            await Assert.That(exception.Message).IsEqualTo(message);
        }
    }

    [Test]
    public async ValueTask Maybe_Expect_ShouldReturnValueOnSome()
    {
        var testValue = Guid.NewGuid().ToString();
        var maybe = Maybe.Some(testValue);

        using (Assert.Multiple())
        {
            var value = await Assert.That(() => maybe.Expect("message")).ThrowsNothing();
            await Assert.That(value).IsEqualTo(testValue);
        }
    }

    [Test]
    [Arguments(2)]
    [Arguments(4)]
    [Arguments(6)]
    public async ValueTask Maybe_IsSomeAnd_ShouldReturnTrueWhenSomeAndPredicateIsTrue(int input)
    {
        var maybe = Maybe.Some(input);

        var result = maybe.IsSomeAnd(v => v % 2 == 0);

        await Assert.That(result).IsTrue();
    }

    [Test]
    [Arguments(1)]
    [Arguments(3)]
    [Arguments(5)]
    public async ValueTask Maybe_IsSomeAnd_ShouldReturnFalseWhenSomeAndPredicateIsFalse(int input)
    {
        var maybe = Maybe.Some(input);

        var result = maybe.IsSomeAnd(v => v % 2 == 0);

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async ValueTask Maybe_IsSomeAnd_ShouldReturnFalseWhenNone()
    {
        Maybe<int> maybe = Maybe.None;

        var result = maybe.IsSomeAnd(v => v % 2 == 0);

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async ValueTask Maybe_Bind_ShouldExecuteBinderWhenInitialIsSome()
    {
        var value = Random.Shared.Next();
        var initial = Maybe.Some(value);

        var result = initial.Bind(v => Maybe.Some(v + 1));

        using (Assert.Multiple())
        {
            await Assert.That(result.IsSome).IsTrue();
            await Assert.That(result.Unwrap()).IsEqualTo(value + 1);
        }
    }

    [Test]
    public async ValueTask Maybe_Bind_ShouldReturnNoneWhenInitialIsNone()
    {
        Maybe<int> initial = Maybe.None;

        var result = initial.Bind(v => Maybe.Some(v + 1));

        await Assert.That(result.IsNone).IsTrue();
    }

    [Test]
    [Arguments(2)]
    [Arguments(4)]
    [Arguments(6)]
    public async ValueTask Maybe_Filter_ShouldReturnSomeWhenInitialIsSomeAndPredicateIsTrue(int input)
    {
        var initial = Maybe.Some(input);

        var result = initial.Filter(v => v % 2 == 0);

        using (Assert.Multiple())
        {
            await Assert.That(result.IsSome).IsTrue();
            await Assert.That(result.Unwrap()).IsEqualTo(input);
        }
    }

    [Test]
    [Arguments(1)]
    [Arguments(3)]
    [Arguments(5)]
    public async ValueTask Maybe_Filter_ShouldReturnNoneWhenInitialIsSomeAndPredicateIsFalse(int input)
    {
        var initial = Maybe.Some(input);

        var result = initial.Filter(v => v % 2 == 0);

        await Assert.That(result.IsNone).IsTrue();
    }

    [Test]
    public async ValueTask Maybe_Filter_ShouldReturnNoneWhenInitialIsNone()
    {
        Maybe<string> initial = Maybe.None;

        var result = initial.Filter(_ => true);

        await Assert.That(result.IsNone).IsTrue();
    }

    [Test]
    public async ValueTask Maybe_Map_ShouldExecuteMapperWhenInitialIsSome()
    {
        var value = Random.Shared.Next();
        var initial = Maybe.Some(value);

        var result = initial.Map(v => v + 1);

        using (Assert.Multiple())
        {
            await Assert.That(result.IsSome).IsTrue();
            await Assert.That(result.Unwrap()).IsEqualTo(value + 1);
        }
    }

    [Test]
    public async ValueTask Maybe_Map_ShouldReturnNoneWhenInitialIsNone()
    {
        Maybe<int> initial = Maybe.None;

        var result = initial.Map(v => v + 1);

        await Assert.That(result.IsNone).IsTrue();
    }

    [Test]
    public async ValueTask Maybe_And_ShouldReturnOtherWhenInitialIsSome()
    {
        var first = Maybe.Some(1);
        var second = Maybe.Some(2);

        var result = first.And(second);

        using (Assert.Multiple())
        {
            await Assert.That(result.IsSome).IsTrue();
            await Assert.That(result).IsEqualTo(second);
        }
    }

    [Test]
    public async ValueTask Maybe_And_ShouldReturnNoneWhenInitialIsNone()
    {
        Maybe<int> first = Maybe.None;
        var second = Maybe.Some(2);

        var result = first.And(second);

        await Assert.That(result.IsNone).IsTrue();
    }

    [Test]
    public async ValueTask Maybe_Or_ShouldReturnSelfWhenSome()
    {
        var first = Maybe.Some(1);
        var second = Maybe.Some(2);

        var result = first.Or(second);

        using (Assert.Multiple())
        {
            await Assert.That(result.IsSome).IsTrue();
            await Assert.That(result).IsEqualTo(first);
        }
    }

    [Test]
    public async ValueTask Maybe_Or_ShouldReturnOtherWhenNone()
    {
        Maybe<int> first = Maybe.None;
        var second = Maybe.Some(2);

        var result = first.Or(second);

        using (Assert.Multiple())
        {
            await Assert.That(result.IsSome).IsTrue();
            await Assert.That(result).IsEqualTo(second);
        }
    }

    [Test]
    public async ValueTask Maybe_OrElse_ShouldReturnSelfWhenSome()
    {
        var first = Maybe.Some(1);
        var second = Maybe.Some(2);

        var result = first.OrElse(() => second);

        using (Assert.Multiple())
        {
            await Assert.That(result.IsSome).IsTrue();
            await Assert.That(result).IsEqualTo(first);
        }
    }

    [Test]
    public async ValueTask Maybe_OrElse_ShouldExecuteValueFactoryWhenNone()
    {
        Maybe<int> first = Maybe.None;
        var second = Maybe.Some(2);

        var result = first.OrElse(() => second);

        using (Assert.Multiple())
        {
            await Assert.That(result.IsSome).IsTrue();
            await Assert.That(result).IsEqualTo(second);
        }
    }

    [Test]
    public async ValueTask Maybe_Xor_ShouldReturnSelfWhenSomeAndOtherIsNone()
    {
        var first = Maybe.Some(1);
        Maybe<int> second = Maybe.None;

        var result = first.Xor(second);

        using (Assert.Multiple())
        {
            await Assert.That(result.IsSome).IsTrue();
            await Assert.That(result).IsEqualTo(first);
        }
    }

    [Test]
    public async ValueTask Maybe_Xor_ShouldReturnOtherWhenNoneAndOtherIsSome()
    {
        Maybe<int> first = Maybe.None;
        var second = Maybe.Some(2);

        var result = first.Xor(second);

        using (Assert.Multiple())
        {
            await Assert.That(result.IsSome).IsTrue();
            await Assert.That(result).IsEqualTo(second);
        }
    }

    [Test]
    public async ValueTask Maybe_Xor_ShouldReturnNoneWhenNoneAndOtherIsNone()
    {
        Maybe<int> first = Maybe.None;
        Maybe<int> second = Maybe.None;

        var result = first.Xor(second);

        await Assert.That(result.IsNone).IsTrue();
    }

    [Test]
    public async ValueTask Maybe_AsEnumerable_ShouldReturnOneElementForSome()
    {
        var some = Maybe.Some(Guid.NewGuid().ToString());

        await Assert.That(some.AsEnumerable().Count()).IsEqualTo(1);
    }

    [Test]
    public async ValueTask Maybe_AsEnumerable_ShouldReturnNoElementsForNone()
    {
        Maybe<string> none = Maybe.None;

        await Assert.That(none.AsEnumerable().Count()).IsEqualTo(0);
    }

    [Test]
    public async ValueTask Maybe_BindOrElse_ShouldApplyBinderOnSome()
    {
        var some = Maybe.Some(10);

        var result = some.BindOrElse(v => Maybe.Some(v + 5), () => Maybe.Some(20));

        using (Assert.Multiple())
        {
            await Assert.That(result.IsSome).IsTrue();
            await Assert.That(result.Unwrap()).IsEqualTo(15);
        }
    }

    [Test]
    public async ValueTask Maybe_BindOrElse_ShouldUseValueFactoryOnNone()
    {
        Maybe<int> none = Maybe.None;

        var result = none.BindOrElse(_ => Maybe.Some(10), () => Maybe.Some(20));

        using (Assert.Multiple())
        {
            await Assert.That(result.IsSome).IsTrue();
            await Assert.That(result.Unwrap()).IsEqualTo(20);
        }
    }

    [Test]
    public async ValueTask Maybe_BindOr_ShouldApplyBinderOnSome()
    {
        var some = Maybe.Some(10);

        var result = some.BindOr(v => Maybe.Some(v + 5), Maybe.Some(20));

        using (Assert.Multiple())
        {
            await Assert.That(result.IsSome).IsTrue();
            await Assert.That(result.Unwrap()).IsEqualTo(15);
        }
    }

    [Test]
    public async ValueTask Maybe_BindOr_ShouldUseAlternativeOnNone()
    {
        Maybe<int> none = Maybe.None;

        var result = none.BindOr(_ => Maybe.Some(10), Maybe.Some(20));

        using (Assert.Multiple())
        {
            await Assert.That(result.IsSome).IsTrue();
            await Assert.That(result.Unwrap()).IsEqualTo(20);
        }
    }

    [Test]
    public async ValueTask Maybe_Match_ShouldExecuteOnSomeDelegateWhenSome()
    {
        var some = Maybe.Some(10);

        var result = some.Match(v => v + 5, () => 100);

        await Assert.That(result).IsEqualTo(15);
    }

    [Test]
    public async ValueTask Maybe_Match_ShouldExecuteOnNoneDelegateWhenNone()
    {
        Maybe<int> none = Maybe.None;

        var result = none.Match(v => v + 5, () => 100);

        await Assert.That(result).IsEqualTo(100);
    }

    [Test]
    public async ValueTask Maybe_Do_ShouldExecuteOnSomeDelegateWhenSome()
    {
        var some = Maybe.Some(10);
        var isSomeExecuted = false;
        var isNoneExecuted = false;

        some.Do(() => isSomeExecuted = true, () => isNoneExecuted = true);

        using (Assert.Multiple())
        {
            await Assert.That(isSomeExecuted).IsTrue();
            await Assert.That(isNoneExecuted).IsFalse();
        }
    }

    [Test]
    public async ValueTask Maybe_Do_ShouldExecuteOnNoneDelegateWhenNone()
    {
        Maybe<int> none = Maybe.None;
        var isSomeExecuted = false;
        var isNoneExecuted = false;

        none.Do(() => isSomeExecuted = true, () => isNoneExecuted = true);

        using (Assert.Multiple())
        {
            await Assert.That(isSomeExecuted).IsFalse();
            await Assert.That(isNoneExecuted).IsTrue();
        }
    }

    [Test]
    public async ValueTask Maybe_DoOnSome_ShouldExecuteDelegateWhenSome()
    {
        var some = Maybe.Some(10);
        var isExecuted = false;

        some.DoOnSome(() => isExecuted = true);

        await Assert.That(isExecuted).IsTrue();
    }

    [Test]
    public async ValueTask Maybe_DoOnSome_ShouldNotExecuteDelegateWhenNone()
    {
        Maybe<int> none = Maybe.None;
        var isExecuted = false;

        none.DoOnSome(() => isExecuted = true);

        await Assert.That(isExecuted).IsFalse();
    }

    [Test]
    public async ValueTask Maybe_DoOnNone_ShouldExecuteDelegateWhenNone()
    {
        Maybe<int> none = Maybe.None;
        var isExecuted = false;

        none.DoOnNone(() => isExecuted = true);

        await Assert.That(isExecuted).IsTrue();
    }

    [Test]
    public async ValueTask Maybe_DoOnNone_ShouldNotExecuteDelegateWhenSome()
    {
        var some = Maybe.Some(10);
        var isExecuted = false;

        some.DoOnNone(() => isExecuted = true);

        await Assert.That(isExecuted).IsFalse();
    }
}
