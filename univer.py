## -- IMPORTS

import http.server;
import socketserver;
import os;

## -- TYPES

class GzipHTTPRequestHandler( http.server.SimpleHTTPRequestHandler ) :

    def guess_type( self, file_path ) :

        if file_path.endswith( ".wasm.gz" ) :

            return "application/wasm";

        return super().guess_type( file_path );

    def send_head( self ) :

        file_path = self.translate_path( self.path );
        file = None;

        if os.path.isdir( file_path ) :

            part_array = self.path.split( '/' );

            if not self.path.endswith( '/' ) :

                self.send_response( 301 );
                self.send_header( "Location", self.path + "/" );
                self.end_headers();

                return None;

            for index in "index.html", "index.htm" :

                index = os.path.join( file_path, index );

                if os.path.exists( index ) :

                    file_path = index;
                    break;

            else:

                return self.list_directory( file_path );

        content_type = self.guess_type( file_path );

        try:

            if file_path.endswith( ".gz" ) :

                file = open( file_path, 'rb' );
                self.send_response( 200 );
                self.send_header( "Content-type", content_type );
                self.send_header( "Content-Encoding", "gzip" );
                file_status = os.fstat( file.fileno() );
                self.send_header( "Content-Length", str( file_status.st_size ) );
                self.end_headers();

                return file;

            file = open( file_path, 'rb' );

        except OSError:

            self.send_error( 404, "File not found" );

            return None;

        try:

            self.send_response( 200 );
            self.send_header( "Content-type", content_type );
            file_status = os.fstat( file.fileno() );
            self.send_header( "Content-Length", str( file_status.st_size ) );
            self.send_header( "Last-Modified", self.date_time_string( file_status.st_mtime ) );
            self.end_headers();

            return file;

        except:

            file.close();
            raise;

## -- STATEMENTS

Handler = GzipHTTPRequestHandler;

port = 8000;

with socketserver.TCPServer( ( "", port ), Handler ) as httpd :

    print( f"Serving on localhost:{ port }..." );
    httpd.serve_forever();
