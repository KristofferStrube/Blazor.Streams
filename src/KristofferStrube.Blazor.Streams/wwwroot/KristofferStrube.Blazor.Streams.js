export function getAttribute(object, attribute) { return object[attribute]; }

export function constructReadableStream(underlyingSource, strategy) {
    return new ReadableStream(underlyingSource, strategy);
}

