export function getAttribute(object, attribute) { return object[attribute]; }

export function constructReadableStream(underlyingSource, strategy) {
    return new ReadableStream(underlyingSource, strategy);
}

export function constructReadableStreamDefaultReader(stream) {
    return new ReadableStreamDefaultReader(stream);
}

export function constructReadableStreamBYOBReader(stream) {
    return new ReadableStreamBYOBReader(stream);
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