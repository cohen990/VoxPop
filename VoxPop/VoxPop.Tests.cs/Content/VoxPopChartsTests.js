/// <reference path="~/Scripts/jasmine.js"/>
/// <reference path="../../Site/Content/VoxPopCharts.js"/>

describe("DecodeHtml given empty string", function () {
    it("returns empty string", function () {
        var result = VoxPopCharts.DecodeHtml("");
        expect(result).toBe("");
    });
});

describe("DecodeHtml given unencoded string", function () {
    it("returns same string", function () {
        var input = "hello world";
        var result = VoxPopCharts.DecodeHtml(input);
        expect(result).toBe(input);
    });
});

describe("DecodeHtml given encoded string", function() {
    it("returns decoded string", function() {
        var input = "43 + &lt;&gt; things&#39;&quot;:D";
        var result = VoxPopCharts.DecodeHtml(input);
        expect(result).toBe("43 + <> things'\":D");
    });
});

describe("GetPollData given 'option1' and 2", function () {
    it("sets value to 2", function() {
        var inputName = "option1";
        var inputNumber = 2;
        var result = VoxPopCharts.GetPollData(inputName, inputNumber);
        expect(result.value).toBe(2);
    });
});

describe("GetPollData given 'option1' and 2", function () {
    it("sets label to option1", function() {
        var inputName = "option1";
        var inputNumber = 2;
        var result = VoxPopCharts.GetPollData(inputName, inputNumber);
        expect(result.label).toBe("option1");
    });
});

describe("GetPollData given 'option1' and 2", function () {
    it("sets color to a hex colour", function() {
        var inputName = "option1";
        var inputNumber = 2;
        var result = VoxPopCharts.GetPollData(inputName, inputNumber);
        expect(result.color).toContain("#");
    });
});

describe("GetPollData given 'option1' and 2", function () {
    it("sets highlight to a hex colour", function() {
        var inputName = "option1";
        var inputNumber = 2;
        var result = VoxPopCharts.GetPollData(inputName, inputNumber);
        expect(result.highlight).toContain("#");
    });
});

describe("Call GetPollData twice", function () {
    it("sets color to two different colours", function() {
        var inputName = "option1";
        var inputNumber = 2;
        var initialResult = VoxPopCharts.GetPollData(inputName, inputNumber);
        var result = VoxPopCharts.GetPollData(inputName, inputNumber);
        debugger;
        expect(result.color === initialResult.color).toBeFalsy();
    });
});

describe("Call GetPollData twice", function () {
    it("sets highlight to two different colours", function() {
        var inputName = "option1";
        var inputNumber = 2;
        var initialResult = VoxPopCharts.GetPollData(inputName, inputNumber);
        var result = VoxPopCharts.GetPollData(inputName, inputNumber);
        debugger;
        expect(result.highlight === initialResult.highlight).toBeFalsy();
    });
});

describe("Call GetPollData exhausting color list", function () {
    it("sets final color to #ffd1d1", function() {
        var inputName = "option1";
        var inputNumber = 2;
        VoxPopCharts.GetPollData(inputName, inputNumber);
        VoxPopCharts.GetPollData(inputName, inputNumber);
        VoxPopCharts.GetPollData(inputName, inputNumber);
        VoxPopCharts.GetPollData(inputName, inputNumber);
        VoxPopCharts.GetPollData(inputName, inputNumber);
        VoxPopCharts.GetPollData(inputName, inputNumber);
        VoxPopCharts.GetPollData(inputName, inputNumber);
        VoxPopCharts.GetPollData(inputName, inputNumber);
        VoxPopCharts.GetPollData(inputName, inputNumber);
        VoxPopCharts.GetPollData(inputName, inputNumber);
        VoxPopCharts.GetPollData(inputName, inputNumber);
        VoxPopCharts.GetPollData(inputName, inputNumber);
        VoxPopCharts.GetPollData(inputName, inputNumber);
        VoxPopCharts.GetPollData(inputName, inputNumber);
        VoxPopCharts.GetPollData(inputName, inputNumber);
        VoxPopCharts.GetPollData(inputName, inputNumber);
        VoxPopCharts.GetPollData(inputName, inputNumber);
        VoxPopCharts.GetPollData(inputName, inputNumber);
        var result = VoxPopCharts.GetPollData(inputName, inputNumber);
        expect(result.color).toBe("#FFd1d1");
    });
});