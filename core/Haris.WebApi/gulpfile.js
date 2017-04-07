var gulp = require('gulp');

gulp.task('deploy-file', function () {
     gulp.src('Content/**')
    .pipe(gulp.dest('../Haris.HostApp/bin/Debug/Content'));
     gulp.src('Views/**')
        .pipe(gulp.dest('../Haris.HostApp/bin/Debug/Views'));
});

gulp.task('watch', function () {
    gulp.watch("Content/**", ['deploy-file']);
    gulp.watch("Views/**", ['deploy-file']);
});

gulp.task('default', ['deploy-file']);