import cherrypy
import json
 
class DataView(object):
    exposed = True
 
    @cherrypy.tools.accept(media='application/json')
 
    def POST(self):
        rawData = cherrypy.request.body.read(int(cherrypy.request.headers['Content-Length']))
        requestData = json.loads(rawData)
        requestId = requestData['id']
        email = requestData['email']
        classification = '1_1;2_3;4_1'.split(";")
# do something with b, in this case I am returning it inside another object
        return json.dumps({'id': requestId, 'res': classification})
 
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
    cherrypy.quickstart(DataView(), '/email_classification/', conf)