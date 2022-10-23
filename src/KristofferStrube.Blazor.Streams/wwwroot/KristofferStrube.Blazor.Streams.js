export function getAttribute(object, attribute) { return object[attribute]; }

export function setAttribute(object, attribute, value) { return object[attribute] = value; }

export function elementAt(array, index) { return array.at(index); }

export function constructReadableStream(underlyingSource, strategy) {
    if (underlyingSource == null) {
        if (strategy == null) {
            return new ReadableStream();
        }
        return new ReadableStream(null, {
            highWaterMark: strategy.highWaterMark,
            size: (chunk) => strategy.objRef.invokeMethod('InvokeSize', DotNet.createJSObjectReference(chunk))
        });
    }
    var source = {
        start(controller) {
            underlyingSource.objRef.invokeMethodAsync('InvokeStart', DotNet.createJSObjectReference(controller));
        },
        pull(controller) {
            underlyingSource.objRef.invokeMethodAsync('InvokePull', DotNet.createJSObjectReference(controller));
        },
        cancel(controller) {
            underlyingSource.objRef.invokeMethodAsync('InvokeCancel', DotNet.createJSObjectReference(controller));
        },
    };
    if (strategy == null) {
        return new ReadableStream(source);
    }
    return new ReadableStream(source, {
        highWaterMark: strategy.highWaterMark,
        size: (chunk) => strategy.objRef.invokeMethod('InvokeSize', DotNet.createJSObjectReference(chunk))
    });
}

export function constructReadableStreamDefaultReader(stream) {
    return new ReadableStreamDefaultReader(stream);
}

export function constructReadableStreamBYOBReader(stream) {
    return new ReadableStreamBYOBReader(stream);
}

export function constructReadableWritablePair(readable, writable) {
    return { readable: readable, writable: writable };
}

export function constructWritableStream(underlyingSource, strategy) {
    return new WritableStream(underlyingSource, strategy);
}

export function constructWritableStreamDefaultReader(stream) {
    return new WritableStreamDefaultReader(stream);
}

export function constructByteArray(size) {
    return new Uint8Array(size);
}

export function byteArray(object) {
    var bytes = new Uint8Array(object);
    return bytes;
}