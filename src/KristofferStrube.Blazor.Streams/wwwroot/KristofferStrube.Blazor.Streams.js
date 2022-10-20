export function getAttribute(object, attribute) { return object[attribute]; }

export function setAttribute(object, attribute, value) { return object[attribute] = value; }

export function elementAt(array, index) { return array.at(index); }

export function constructReadableStream(underlyingSource, strategy) {
    return new ReadableStream(underlyingSource, strategy);
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