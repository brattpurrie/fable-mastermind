const path = require("path");

function resolve(relativePath) {
    return path.join(__dirname, relativePath);
}

module.exports = {
    entry: resolve("test/test.fsproj"),
    outDir: resolve("test/output"),
    babel: {
        "plugins": ["@babel/plugin-transform-modules-commonjs"]
    }
}