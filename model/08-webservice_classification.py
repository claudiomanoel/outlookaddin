import cherrypy
import json
 
class DataView(object):
    exposed = True
 
    @cherrypy.tools.accept(media='application/json')
 
    def POST(self):
        rawData = cherrypy.request.body.read(int(cherrypy.request.headers['Content-Length']))
        b = json.loads(rawData)
# do something with b, in this case I am returning it inside another object
        return json.dumps({'x': 4, 'c': b})
 
def CORS():
    cherrypy.response.headers["Access-Control-Allow-Origin"] = "*"
    
 
if __name__ == '__main__':
    conf = {
        '/': {
            'request.dispatch': cherrypy.dispatch.MethodDispatcher(),
            'tools.CORS.on': True,
        }
    }
    cherrypy.tools.CORS = cherrypy.Tool('before_handler', CORS)
    cherrypy.quickstart(DataView(), '/d/', conf)